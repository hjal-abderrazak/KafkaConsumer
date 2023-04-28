using KafkaConsumer.DAL.Repositories;
using KafkaConsumer.Models;

namespace KafkaConsumer.Interfaces
{
    public interface IProductionLineRepository:IRepository<ProductionLine>
    {
        //if you want to add a custom method that not found in Irepository, you can add here 
        public void Update(ProductionLine productionLine);
    }
}
