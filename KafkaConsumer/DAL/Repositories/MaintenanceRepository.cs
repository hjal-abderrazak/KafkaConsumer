using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;
using Microsoft.IdentityModel.Tokens;

namespace KafkaConsumer.DAL.Repositories
{
    public class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        private readonly AppDbContext context;
        public MaintenanceRepository(AppDbContext context) : base(context)
        {
            this.context = context; 
        }

        public void Update(Maintenance maintenance)
        {
            var maintenanceToUpdate = this.context.maintenances.FirstOrDefault(m => m.Id == maintenance.Id);
           
            maintenanceToUpdate.MaintainedTime = maintenance.MaintainedTime;
            maintenanceToUpdate.MachineId = maintenance.MachineId;
            maintenanceToUpdate.Comment = maintenance.Comment;
            maintenanceToUpdate.MachineId = maintenance.MachineId;
            context.SaveChanges();
        }
    }
}
