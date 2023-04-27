using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;

namespace KafkaConsumer.DAL.Repositories
{
    public class FactoryRepository : Repository<Factory>, IFactoryRepository
    {
        public FactoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
