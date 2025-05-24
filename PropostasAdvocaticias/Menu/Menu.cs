using PropostasAdvocaticias.Domain.UseCases.Client;
using PropostasAdvocaticias.Infrastructure.UseCases.Docx;
using PropostasAdvocaticias.Menu;       // (já estava implícito)

public static class Menu
{
    public static void Show()
    {
        Console.Clear();
        Console.Title = "Propostas de Honorários";
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine($"WorkingDir: {Directory.GetCurrentDirectory()}");


        var escolhidas = HandleMenuEngagements.Show();

        Console.WriteLine("\nServiços selecionados:");
        foreach (var s in escolhidas)
            Console.WriteLine($"{s.Title} – {s.Description}");

        Console.WriteLine("\nPressione qualquer tecla para cadastrar o cliente…");
        Console.ReadKey();

        var newClient = SetClient.NewClient();
        DocxService.CreateDocx(escolhidas, client);
    }
}