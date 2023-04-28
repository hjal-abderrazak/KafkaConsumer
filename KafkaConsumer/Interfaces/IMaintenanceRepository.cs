using KafkaConsumer.Models;

namespace KafkaConsumer.Interfaces
{
    public interface IMaintenanceRepository:IRepository<Maintenance>
    {
        //if you want to add a custom method that not found in Irepository, you can add here 
        public void Update(Maintenance maintenance);
    }
}
