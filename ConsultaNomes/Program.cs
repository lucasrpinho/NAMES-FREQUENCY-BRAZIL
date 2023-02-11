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
            char sexo = Console.ReadLine()[0];
            if (string.IsNullOrEmpty(nome))
            {
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
            Nomes api = new Nomes();
            NomeStatsMax[] obj = await api.Get(nome, sexo);

            if (obj == null) { throw new Exception("A requisição não obteve resposta!"); }
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
