using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;

namespace KafkaConsumer.DAL.Repositories
{
    public class MachineRepository : Repository<Machine>, ImachineRepository
    {
        public MachineRepository(AppDbContext context) : base(context)
        {
        }

    }
}
