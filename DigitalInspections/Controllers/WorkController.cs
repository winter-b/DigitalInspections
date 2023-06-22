using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;
using DigitalInspectionsWebApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DigitalInspectionsWebApi.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkService _workService;
        private readonly IAuthService _authService;

        public WorkController(IWorkService workService, IAuthService authService)
        {
            _workService = workService;
            _authService = authService;
        }

        [HttpGet]
        [Route("Machine/Works")]
        public ActionResult<List<Work>> GetWorks([FromQuery] Guid machineId, [FromQuery] int limit, [FromQuery] int offset, [FromHeader] string token)
        {
            Console.WriteLine("Getting works from machine: " + machineId);
            if (!_authService.UserTokenValid(token, "Worker"))
            {
                return Unauthorized();
            }
            List<Work> works = _workService.GetWorks(machineId, limit, offset);
            return Ok(works);
        }
        [HttpGet]
        [Route("Machine/Work")]
        public ActionResult<Work> GetWork([FromQuery] Guid workId, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Worker"))
            {
                return Unauthorized();
            }
            Work work = _workService.GetWork(workId);
            if (work == null)
            {
                return NotFound();
            }
            return Ok(work);
        }
        [HttpPost]
        [Route("Machine/Work")]
        [Consumes("multipart/form-data")]
        public ActionResult<Work> CreateWork([FromForm] WorkRequest workRequest, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Worker"))
            {
                return Unauthorized();
            }
            string addedBy = _authService.GetUsernameFromToken(token);
            Work createdWork = _workService.CreateWork(workRequest, addedBy);
            return Ok(createdWork);
        }
        [HttpPut]
        [Route("Machine/Work")]
        public ActionResult<Work> UpdateWork([FromForm] WorkRequest workRequest, [FromQuery] Guid workId, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Worker"))
            {
                return Unauthorized();
            }
            Work updatedWork = _workService.UpdateWork(workRequest, workId);
            if (updatedWork == null)
            {
                return NotFound();
            }
            return Ok(updatedWork);
        }
        [HttpDelete]
        [Route("Machine/Work")]
        public ActionResult<Work> DeleteWork([FromQuery]Guid workId, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Manager"))
            {
                return Unauthorized();
            }
            _workService.DeleteWork(workId);
            return Ok();
        }
    }
}
