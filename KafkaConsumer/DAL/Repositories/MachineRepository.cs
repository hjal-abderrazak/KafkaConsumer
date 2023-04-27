using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;

namespace KafkaConsumer.DAL.Repositories
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        public MachineRepository(AppDbContext context) : base(context)
        {
        }

    }
}
