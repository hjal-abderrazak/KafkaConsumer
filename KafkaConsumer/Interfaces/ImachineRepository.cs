using KafkaConsumer.Models;

namespace KafkaConsumer.Interfaces
{
    public interface IMachineRepository:IRepository<Machine>
    {
        //if you want to add a custom method that not found in Irepository, you can add here 
        public void Update(Machine machine);

        public ICollection<Machine> getAlluserMachines(Guid userId);
    }
}
