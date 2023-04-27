namespace KafkaConsumer.Models
{
    public class ProductionLine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Machine> machines { get;  }
        public Guid? FactoryId { get; set; }
        public Factory? Factory { get; set; }
        public ProductionLine()
        {
            
        }
    }
}
