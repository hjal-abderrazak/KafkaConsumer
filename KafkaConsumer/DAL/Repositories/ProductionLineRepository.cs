using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;

namespace KafkaConsumer.DAL.Repositories
{
    public class ProductionLineRepository : Repository<ProductionLine>, IProductionLineRepository
    {
        private readonly AppDbContext context;
        public ProductionLineRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(ProductionLine productionLine)
        {
            var productionLineToUpdate = this.context.ProductionLines.FirstOrDefault(p => p.Id == productionLine.Id);
           
            productionLineToUpdate.Name  = productionLine.Name;
            productionLineToUpdate.FactoryId = productionLine.FactoryId;
            context.SaveChanges();
            
        }




    }
}
