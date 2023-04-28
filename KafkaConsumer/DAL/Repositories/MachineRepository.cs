using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;

namespace KafkaConsumer.DAL.Repositories
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    { 
        private readonly AppDbContext context;
        public MachineRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(Machine machine)
        {
            var machineToUpdate = this.context.Machines.FirstOrDefault(m => m.Id == machine.Id);
            
            machineToUpdate.Name = machine.Name;
            machineToUpdate.StartDate = machine.StartDate;
            machineToUpdate.Description = machine.Description;
            machineToUpdate.MaintainedCount = machine.MaintainedCount;
            machineToUpdate.ProductionLineId = machine.ProductionLineId;
            machineToUpdate.LastMaintainedTime = machine.LastMaintainedTime;
            context.SaveChanges();
        }
    }
}
