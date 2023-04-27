using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;

namespace KafkaConsumer.DAL.Repositories
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
