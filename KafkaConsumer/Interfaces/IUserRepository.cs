using KafkaConsumer.Models;

namespace KafkaConsumer.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        //if you want to add a custom method that not found in Irepository, you can add here 

    }
}
