using KafkaConsumer.Models;
using Microsoft.EntityFrameworkCore;

namespace KafkaConsumer.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ):base(options)
        {
            
        }
        public DbSet<MachineDetails> MachineDetails { get; set; }
    }
}
