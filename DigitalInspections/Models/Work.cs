namespace DigitalInspectionsWebApi.Models
{
    public class Work
    {
        public Work()
        {
        }

        public Work(Guid id, string name, string category, string description, string addedBy, DateTimeOffset added, DateTimeOffset lastUpdated, DateTimeOffset nextInspection, bool isInspected, Guid machineId)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            AddedBy = addedBy;
            Added = added;
            LastUpdated = lastUpdated;
            NextInspection = nextInspection;
            IsInspected = isInspected;
            MachineId = machineId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // TODO: Change to enum
        public string Description { get; set; }
        public string AddedBy { get; set; }
        public DateTimeOffset Added { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public DateTimeOffset NextInspection { get; set; }
        public bool IsInspected { get; set; }
        public Guid MachineId { get; set; }
    }
}
