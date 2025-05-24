using System.Text.Json;
using PropostasAdvocaticias.Domain.Entities;

namespace PropostasAdvocaticias.Infrastructure.UseCases.Engagement;


public static class SetEngagement
{
    private const string AssetsFolder = "Assets";
    private const string EngagementSubfolder = "Engagement";
    private const string JsonPattern = "*.json";


    private static readonly JsonSerializerOptions ReadOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly JsonSerializerOptions PrintOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static IReadOnlyList<LawArea> LoadEngagement()
    {
        var engagementDir = Path.Combine(AppContext.BaseDirectory, AssetsFolder, EngagementSubfolder);
        var result = new List<LawArea>();

        foreach (string filePath in Directory.EnumerateFiles(engagementDir, JsonPattern, SearchOption.TopDirectoryOnly))
        {
            string jsonContent = File.ReadAllText(filePath);
            var lawArea = JsonSerializer.Deserialize<LawArea>(jsonContent, ReadOptions);

            if (lawArea != null)
            {
                result.Add(lawArea);
                
                Console.WriteLine($"Área do Direito: {lawArea.Title}");
                Console.WriteLine("Engagements:");

                foreach (var engagement in lawArea.Engagements)
                {
                    Console.WriteLine($"- Título: {engagement.Title}");
                    Console.WriteLine($"  Descrição: {engagement.Description}");
                }

                Console.WriteLine(); // Adiciona uma linha em branco para separar as áreas
            }
        }

        return result;
    }
}