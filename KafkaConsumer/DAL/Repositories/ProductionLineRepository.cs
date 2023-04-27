using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;

namespace KafkaConsumer.DAL.Repositories
{
    public class ProductionLineRepository : Repository<ProductionLine>, IProductionLineRepository
    {
        public ProductionLineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
