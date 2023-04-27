using KafkaConsumer.Models;
using Microsoft.EntityFrameworkCore;

namespace KafkaConsumer.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Factory> Factorys { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
        public DbSet<Maintenance> maintenances { get; set; }
        public DbSet<StatusRecord> StatusRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //department and employee relation
            modelBuilder.Entity<Department>().HasMany(d => d.Users)
                .WithOne(u => u.Department).HasForeignKey(u=>u.DepartmentId);

            //machine and productionline relation
            modelBuilder.Entity<ProductionLine>().HasMany(p=>p.machines)
                .WithOne(m=>m.ProductionLine).HasForeignKey(m=>m.ProductionLineId);

            //machine and status record relation 
            modelBuilder.Entity<Machine>().HasMany(m=>m.StatusRecords)
                .WithOne(s=>s.Machine).HasForeignKey(s=>s.MachineId);

            //productionLine and Factory relation
            modelBuilder.Entity<Factory>().HasMany(f => f.productionLines)
                .WithOne(p => p.Factory).HasForeignKey(p => p.FactoryId)
                .IsRequired(false);

            //machine and maintenance relation 
            modelBuilder.Entity<Machine>().HasMany(m => m.Maintenances)
                .WithOne(m => m.Machine).HasForeignKey(m => m.MachineId);

            //technicien and machine relation 
            modelBuilder.Entity<User>().HasMany(tehnicien => tehnicien.Maintenances)
                .WithOne(m => m.Technicien).HasForeignKey(m => m.TechnicienId);


        }
    }
}
