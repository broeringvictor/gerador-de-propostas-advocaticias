using System;
using PropostasAdvocaticias.Domain.Entities;
using System.Linq;

namespace PropostasAdvocaticias.Domain.UseCases.Client
{
    public static class SetClient
    {
        public static NewClient()
        {
            Console.Write("Nome completo: ");
            string fullName = Console.ReadLine()!;

            Console.Write("E-mail: ");
            string email = Console.ReadLine()!;

            decimal price;
            while (true)
            {
                Console.Write("Preço: ");
                var priceInput = Console.ReadLine()!;
                if (decimal.TryParse(priceInput, out price))
                    break;
                Console.WriteLine("Preço inválido. Digite apenas números e decimais.");
            }

            int discount;
            while (true)
            {
                Console.Write("Desconto (%): ");
                var discountInput = Console.ReadLine()!;
                if (int.TryParse(discountInput, out discount) && discount >= 0 && discount <= 100)
                    break;
                Console.WriteLine("Desconto inválido. Informe um valor inteiro entre 0 e 100.");
            }

            var client = new Entities.Client(fullName, email, price, discount);

            Console.WriteLine($"Nome: {client.FullName}\nE-mail: {client.Email}\nPreço: {client.Price:C}\nDesconto: {client.Discount}%");
            
            return client;
        }
    }
}