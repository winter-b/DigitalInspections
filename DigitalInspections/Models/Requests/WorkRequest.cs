using Microsoft.AspNetCore.Mvc;

namespace DigitalInspectionsWebApi.Models.Requests
{
    public class WorkRequest
    {
        [FromForm(Name = "name")]
        public string Name { get; set; }
        [FromForm(Name = "category")]
        public string Category { get; set; } // TODO: Change to enum
        [FromForm(Name = "description")]
        public string Description { get; set; }
        [FromForm(Name = "addedBy")]
        public string AddedBy { get; set; }
        [FromForm(Name = "added")]
        public DateTimeOffset Added { get; set; }
        [FromForm(Name = "lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }
        [FromForm(Name = "nextInspection")]
        public DateTimeOffset? NextInspection { get; set; }
        [FromForm(Name = "isInspected")]
        public bool? IsInspected { get; set; }
        [FromForm(Name = "machineId")]
        public Guid MachineId { get; set; }
    }
}
