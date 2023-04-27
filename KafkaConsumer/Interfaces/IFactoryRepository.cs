using KafkaConsumer.Models;

namespace KafkaConsumer.Interfaces
{
    public interface IFactoryRepository:IRepository<Factory>
    {
        //if you want to add a custom method that not found in Irepository, you can add here 

    }
}
