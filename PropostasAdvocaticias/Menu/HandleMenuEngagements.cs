using PropostasAdvocaticias.Domain.Entities;
using PropostasAdvocaticias.Infrastructure.UseCases.Engagement;

namespace PropostasAdvocaticias.Menu;

public static class HandleMenuEngagements
{
    /// <summary>
    /// Mostra os menus, coleta seleções e devolve a lista final de serviços escolhidos.
    /// </summary>
    public static List<Engagement> Show()
    {
        var areas = SetEngagement.LoadEngagement();
        var selectedServices = new List<Engagement>();

        while (true)
        {
            // menu de áreas
            int areaIdx = AskOption(
                "Escolha uma área do direito",
                areas.Select(a => a.Title));

            if (areaIdx == 0)                      
                break;

            var area = areas[areaIdx - 1];

            // submenu de serviços
            int svcIdx = AskOption(
                $"Área: {area.Title}\nServiços disponíveis",
                area.Engagements.Select(e => $"{e.Title}: {e.Description}"),
                backLabel: "Voltar");

            if (svcIdx == 0)                
                continue;

            var svc = area.Engagements[svcIdx - 1];
            selectedServices.Add(svc);

            Console.WriteLine($"\n✔ Adicionado: {svc.Title}");
            Console.WriteLine("Pressione qualquer tecla…");
            Console.ReadKey();
        }

        return selectedServices;                
    }


    private static int AskOption(string header, IEnumerable<string> items, string backLabel = "Sair")
    {
        var list = items.ToList();

        while (true)
        {
            Console.Clear();
            Console.WriteLine(header);
            Console.WriteLine(new string('-', header.Length));

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}. {list[i]}");

            Console.WriteLine($"0. {backLabel}");
            Console.Write("\nOpção: ");

            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice >= 0 && choice <= list.Count)
                return choice;

            Console.WriteLine("❌ Entrada inválida. Tente novamente…");
            Console.ReadKey();
        }
    }
}
