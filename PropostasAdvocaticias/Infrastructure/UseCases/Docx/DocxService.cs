using System.Globalization;
using System.IO;
using PropostasAdvocaticias.Domain.Entities;
using Xceed.Words.NET;

namespace PropostasAdvocaticias.Infrastructure.UseCases.Docx;

public static class DocxService
{
    private const string AssetsFolder = "Assets";
    private const string EngagementSubfolder = "Model";
    private const string ModelDocx = "modelo.docx";
    private static readonly CultureInfo Culture = new CultureInfo("pt-BR");

    public static void CreateDocx(List<Domain.Entities.Engagement> engagements, Client client)
    {
        // Get the full path to the template file
        var modelPath = Path.Combine(AppContext.BaseDirectory, AssetsFolder, EngagementSubfolder, ModelDocx);
        
        if (!File.Exists(modelPath))
        {
            throw new FileNotFoundException("Template file not found", modelPath);
        }

        // Initialize replacements dictionary
        var replacements = new Dictionary<string, string>
        {
            {"[nome_completo]", client.FullName},
            {"[valor]", client.Price.ToString("C", Culture)},
            {"[data_por_extenso]", client.Date.ToString("dd 'de' MMMM 'de' yyyy", Culture)}
        };

        // Add discount if applicable
        if (client.Discount > 0)
        {
            decimal discountedValue = client.Price * (100 - client.Discount) / 100;
            string discountText = $"Caso opte por pagar à vista, oferecemos um desconto de {client.Discount}% " +
                                 $"no valor total da proposta, ficando {discountedValue.ToString("C", Culture)}.";
            
            replacements.Add("[desconto]", discountText);
        }

        // Process engagements
        int engagementCount = 1;
        foreach (var engagement in engagements)
        {
            replacements.Add($"[engagement_title_{engagementCount}]", engagement.Title);
            replacements.Add($"[engagement_description_{engagementCount}]", engagement.Description);
            engagementCount++;
        }

        // Load and process the document
        using (DocX document = DocX.Load(modelPath))
        {
            foreach (var replacement in replacements)
            {
                document.ReplaceText(replacement.Key, replacement.Value);
            }

            // Save the modified document
            string outputFileName = $"Proposta_{client.FullName}_{DateTime.Now:yyyyMMddHHmmss}.docx";
            string outputPath = Path.Combine(AppContext.BaseDirectory, AssetsFolder, "Output", outputFileName);
            
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            document.SaveAs(outputPath);
        }
    }
}