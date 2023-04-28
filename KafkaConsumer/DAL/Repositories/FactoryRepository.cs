using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;

namespace KafkaConsumer.DAL.Repositories
{
    public class FactoryRepository : Repository<Factory>, IFactoryRepository
    {
        private readonly AppDbContext context;
        public FactoryRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(Factory factory)
        {
            var factoryToUpdate = this.context.Factorys.FirstOrDefault(f => f.Id == factory.Id);
           
            factoryToUpdate.Name = factory.Name;
            factoryToUpdate.Reference = factory.Reference;  
            context.SaveChanges();
            
        }
    }
}
