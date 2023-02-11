using System;
using NamesFrequency_Brazil.Models;
using NamesFrequency_Brazil.Controllers;
using System.Net.NetworkInformation;

namespace ConsultaNomes
{
    class Program
    {
        async static Task Main(string[] args)
        {
            int retrys = 0;
            Console.WriteLine("Esse projeto é apenas um teste para aprendizado do .NET 7.0.");
            Console.WriteLine(Environment.NewLine + "A funcionalidade é digitar um nome e um sexo e a API irá retornar as ocorrências desse nome no Brasil com base nos dados do IBGE.");
            Console.WriteLine(Environment.NewLine + "Digite o nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o sexo (M / F): ");
            string sexo = Console.ReadLine();
            if (string.IsNullOrEmpty(nome) || nome.Length < 3)
            {
                Console.WriteLine("O nome não pode ser vazio ou conter menos que três caracteres!");
                for (retrys = 0; retrys < 3; retrys++)
                {
                    Console.WriteLine(Environment.NewLine + "Digite um nome: "); nome = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nome)) { break; }
                    else if (retrys >=2 && string.IsNullOrEmpty(nome)){ 
                        Console.WriteLine("Tentativas esgotadas!");
                        Console.WriteLine("A aplicação será encerrada em 3 segundos...");
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                    }
                }
            }

            if (sexo == string.Empty) { 
                Console.WriteLine("Digite o sexo: (M / F): ");
                sexo = Console.ReadLine();
            }
            else
            {
                if (sexo[0].ToString().ToUpper() != "M" && sexo[0].ToString().ToUpper() != "F")
                {
                    Console.WriteLine("O sexo digitado é inválido!");
                    Console.WriteLine("Digite o sexo (M / F): ");
                    sexo = Console.ReadLine();
                }
            }
            Nomes api = new Nomes();
            NomeStatsMax[] obj = await api.Get(nome, sexo);

            if (obj == null || obj.Count() == 0) { Console.WriteLine("A busca não encontrou resultados para o nome informado. "); Console.ReadKey(); }
            else
            {
                Console.WriteLine(Environment.NewLine + "-------------- RESULTADO -------------- ");
                Console.WriteLine("Rank: " + obj[0].rank);
                Console.WriteLine("Nome: " + obj[0].nome);
                Console.WriteLine("Percentual: " + obj[0].percentual);
                Console.WriteLine("Sexo: " + obj[0].sexo);
                Console.WriteLine("Estado com maior incidência do nome: " + obj[0].ufMax);
                Console.WriteLine("Proporção no Estado com maior incidência: " + obj[0].ufMaxProp);
                Console.WriteLine("Nomes parecidos: " + obj[0].nomes);
            }

        }
    }
}
