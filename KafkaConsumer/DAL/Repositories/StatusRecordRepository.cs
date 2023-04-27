using KafkaConsumer.Interfaces;
using KafkaConsumer.Models;

namespace KafkaConsumer.DAL.Repositories
{
    public class StatusRecordRepository : Repository<StatusRecord>, IStatusRecordRepository
    {
        public StatusRecordRepository(AppDbContext context) : base(context)
        {
        }
    }
}
