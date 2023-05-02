using Confluent.Kafka;
using KafkaConsumer.Helper;
using KafkaConsumer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace KafkaConsumer.DAL
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHubContext<MyHub> _hubContext;
        public KafkaConsumerService(AppDbContext dbContext, IHubContext<MyHub> hubContext)
        {
            _appDbContext = dbContext;
            _hubContext = hubContext;
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
                            var lstUser = getListofUser(record.MachineId);
                            if(lstUser != null)
                            {
                                foreach (var userId in lstUser)
                                {
                                     _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveMessage", record.Description);
                                }
                            }
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
        private IEnumerable<Guid> getListofUser(Guid machineId)
        {  
            var lst = new List<Guid>();

            lst = ( from user in _appDbContext.Users
                          join factory in _appDbContext.Factorys
                          on user.Id equals factory.Id
                          join productionline in _appDbContext.ProductionLines
                          on factory.Id equals productionline.FactoryId 
                          join machine  in _appDbContext.Machines 
                          on productionline.Id equals machine.ProductionLineId
                          select user.Id).ToList();
            return lst;
            
        }


    }
}
