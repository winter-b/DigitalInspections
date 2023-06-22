namespace DigitalInspectionsWebApi.Models
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTimeOffset LastUpdated { get; set; }

        public Machine(Guid id, string name, string category, string description, DateTimeOffset lastUpdated)
        {
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            LastUpdated = lastUpdated;
        }
        public Machine()
        {
        }
    }
}
