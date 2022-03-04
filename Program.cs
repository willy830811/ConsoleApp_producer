using Confluent.Kafka;
using System;

namespace ConsoleApp_producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All
            };
            //傳送訊息

            using (var kafkaProducer = new KafkaProducer(config, "topic-d"))
            {
                var result = kafkaProducer.Produce<object>("a", new { name = "豬八戒3" }, 1);

            }
            Console.WriteLine("訊息傳送成功");
        }
    }
}