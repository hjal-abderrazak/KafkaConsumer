using Confluent.Kafka;
using KafkaConsumer.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace KafkaConsumer.DAL
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly AppDbContext _appDbContext;
        public KafkaConsumerService(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
        Task IHostedService.StartAsync(CancellationToken stoppingToken)
        {
            var topic = "temperature-readings";

            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "my-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            try
            {
                using var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build();
                consumerBuilder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(cancelToken.Token);
                        var record = JsonSerializer.Deserialize<StatusRecord>(consumer.Message.Value);
                        if(record != null)
                        {
                            SaveToDatabase(record);
                            Debug.WriteLine($"Detail Id:{record?.Id}");
                        }                       
                      
                    }
                }
                catch (OperationCanceledException)
                {
                    consumerBuilder.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private void SaveToDatabase(StatusRecord record)
        {

            {
                _appDbContext.StatusRecords.Add(record);
                _appDbContext.SaveChanges();
            }
        }


    }
}
