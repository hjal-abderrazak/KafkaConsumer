﻿namespace KafkaConsumer.Models
{
    public class Machine
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public short MaintainedCount { get; set; }
        public DateTime LastMaintainedTime { get; set; }

        public Guid ProductionLineId { get; set; }
        public ProductionLine ProductionLine { get; set; }
        public ICollection<StatusRecord> StatusRecords { get; }
        public List<Maintenance> Maintenances { get; }
        public Machine()
        {

        }
    }
}
