namespace KafkaConsumer.Models
{
    public class StatusRecord
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Temperature { get; set; }
        public string? NiveauCo2 { get; set; }
        public DateTime TemperatureDate { get; set; }
        public Guid MachineId { get; set; }
        public Machine Machine { get; set; }
        public StatusRecord() { }

        
    }
}
