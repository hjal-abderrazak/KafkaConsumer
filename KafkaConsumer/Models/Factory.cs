namespace KafkaConsumer.Models
{
    public class Factory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public ICollection<User> Users { get; } = new List<User>();

        public ICollection<ProductionLine> productionLines { get; } = new List<ProductionLine>();
        public Factory()
        {
            
        }


    }
}
