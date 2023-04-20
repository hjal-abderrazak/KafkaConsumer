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
                        var detail = JsonSerializer.Deserialize<MachineDetails>(consumer.Message.Value);
                        if(detail != null)
                        {
                            SaveToDatabase(detail);
                            Debug.WriteLine($"Detail Id:{detail?.Id}");
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
        private void SaveToDatabase(MachineDetails details)
        {

            {
                _appDbContext.MachineDetails.Add(details);
                _appDbContext.SaveChanges();
            }
        }


    }
}
