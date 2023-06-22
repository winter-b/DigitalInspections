using Microsoft.AspNetCore.Mvc;

namespace DigitalInspectionsWebApi.Models.Requests
{
    public class MachineRequest
    {
        [FromForm(Name = "Name")]
        public string Name { get; set; }
        [FromForm(Name = "Category")]
        public string Category { get; set; }
        [FromForm(Name = "Description")]
        public string Description { get; set; }
    }
}
