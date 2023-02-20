using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using NamesFrequency_Brazil.Models;
using System.Text.Json;
using RabbitMQ.Client.Events;
using System.Linq.Expressions;

namespace ConsultaNomes
{
    public static class NomePublisher
    {
        public static void SendData(NomeStatsMax obj)
        {
            try { 
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var dir = @"..\..\..\";
                        var file = Directory.GetFiles(dir, "testMethods.txt");
                        var queue = File.ReadAllText(file[0]);

                        string json = JsonSerializer.Serialize(obj);

                        if (!string.IsNullOrWhiteSpace(queue))
                        {
                            if (queue.Contains("NAMESFREQUENCY")) { queue = queue.Substring(queue.IndexOf("NAMESFREQUENCY"), "NAMESFREQUENCY".Length); }
                        }
                        else { return; }
                        channel.QueueDeclare(queue: queue.ToUpper(),
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        channel.BasicPublish(exchange: string.Empty,
                             routingKey: queue,
                             basicProperties: null,
                             body: Encoding.UTF8.GetBytes(json));

                        // it's just a test. exceptions aren't going to be handled
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            byte[] messageBody = ea.Body.ToArray();
                            string messageText = Encoding.UTF8.GetString(messageBody);
                            Console.WriteLine("Received message: {0}", messageText);

                            using var file = new StreamWriter(@"C:\Projetos VS2022\NamesFrequency_Brazil\namesOutput.txt");

                            file.Write(messageText);

                            Console.WriteLine("Já pode testar usando o arquivo .txt");
                        };

                        channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
