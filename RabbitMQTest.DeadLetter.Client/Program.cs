using System.Text;

using RabbitMQ.Client;

namespace RabbitMQTest.DeadLetter.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            while (true)
            {
                channel.ExchangeDeclare(
                    exchange: "mainexchange",
                    type: ExchangeType.Direct);

                var message = Console.ReadLine();

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("mainexchange", "test", null, body);

                Console.WriteLine($"Send message: {message}");
            }
        }
    }
}