using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PropostasAdvocaticias.Domain.Entities;

namespace PropostasAdvocaticias.Infrastructure.UseCases.Docx;

public static class DocxService
{
    private const string AssetsFolder        = "Assets";
    private const string EngagementSubfolder = "Model";
    private const string ModelDocx           = "ModelDocx.docx";
    private static readonly CultureInfo Culture = new("pt-BR");

    public static void CreateDocx(List<Domain.Entities.Engagement> engagements, Client client)
    {
        // 1. Caminho do template
        var templatePath = Path.Combine(
            AppContext.BaseDirectory, AssetsFolder, EngagementSubfolder, ModelDocx);

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Template não encontrado em {templatePath}");

        // 2. Mapa de substituições
        var replacements = BuildReplacementMap(engagements, client);

        // 3. Copia o template p/ pasta de saída
        var outputFile = $"Proposta_{SanitizeFileName(client.FullName)}_{DateTime.Now:yyyyMMddHHmmss}.docx";
        var outputPath = Path.Combine(AppContext.BaseDirectory, AssetsFolder, "Output", outputFile);
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
        File.Copy(templatePath, outputPath, overwrite: true);

        // 4. Abre e faz as substituições
        using (var doc = WordprocessingDocument.Open(outputPath, true))
        {
            ReplacePlaceholders(doc.MainDocumentPart!.Document, replacements);

            // Se houver cabeçalhos/rodapés:
            foreach (var header in doc.MainDocumentPart.HeaderParts)
                ReplacePlaceholders(header.Header, replacements);
            foreach (var footer in doc.MainDocumentPart.FooterParts)
                ReplacePlaceholders(footer.Footer, replacements);

            doc.MainDocumentPart.Document.Save();
        }

        Console.WriteLine($"Documento gerado em: {outputPath}");
    }

    /* ---------- helpers ---------- */

    private static Dictionary<string, string> BuildReplacementMap(
        List<Domain.Entities.Engagement> engagements, Client client)
    {
        var map = new Dictionary<string, string>
        {
            ["[nome_completo]"]   = client.FullName,
            ["[valor]"]           = client.Price.ToString("C", Culture),
            ["[data_por_extenso]"] = client.Date.ToString("dd 'de' MMMM 'de' yyyy", Culture),
            ["[desconto]"]        = string.Empty
        };

        if (client.Discount > 0)
        {
            var àVista = client.Price * (100 - client.Discount) / 100;
            map["[desconto]"] =
                $"Caso opte por pagar à vista, oferecemos um desconto de {client.Discount}% " +
                $"ficando {àVista.ToString("C", Culture)}.";
        }

        for (int i = 0; i < engagements.Count; i++)
        {
            int n = i + 1;
            map[$"[engagement_title_{n}]"]       = engagements[i].Title;
            map[$"[engagement_description_{n}]"] = engagements[i].Description;
        }

        return map;
    }

    private static void ReplacePlaceholders(OpenXmlElement root, IDictionary<string, string> map)
    {
        // Varre todos os nós <w:t> (Text) e faz Replace no conteúdo
        var textNodes = root.Descendants<Text>();

        foreach (var text in textNodes)
        {
            foreach (var kv in map.Where(kv => text.Text.Contains(kv.Key)))
                text.Text = text.Text.Replace(kv.Key, kv.Value);
        }
    }

    private static string SanitizeFileName(string raw)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            raw = raw.Replace(c, '_');
        return raw;
    }
}
