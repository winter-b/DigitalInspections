using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;
using DigitalInspectionsWebApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DigitalInspectionsWebApi.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineService _machineService;
        private readonly IAuthService _authService;

        public MachineController(IMachineService machineService, IAuthService authService)
        {
            _machineService = machineService;
            _authService = authService;
        }

        [HttpGet]
        [Route("Machine")]
        public ActionResult<Machine> GetMachine([FromQuery] Guid machineId, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Worker"))
            {
                return Unauthorized();
            }
            Machine machine = _machineService.GetMachine(machineId);
            if (machine == null)
            {
                return NotFound();
            }
            return Ok(machine);
        }
        [HttpGet]
        [Route("Machines")]
        public ActionResult<List<Machine>> GetMachines([FromQuery] int limit, [FromQuery] int offset, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Worker"))
            {
                return Unauthorized();
            }
            List<Machine> machines = _machineService.GetMachines(limit, offset);
            return Ok(machines);
        }
        [HttpPost]
        [Route("Machine")]
        [Consumes("multipart/form-data")]
        public ActionResult<Machine> CreateMachine([FromForm] MachineRequest machine, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Manager"))
            {
                return Unauthorized();
            }
            Machine createdMachine = _machineService.CreateMachine(machine);
            return Ok(createdMachine);
        }
        [HttpPut]
        [Route("Machine")]
        [Consumes("multipart/form-data")]
        public ActionResult<Machine> UpdateMachine([FromForm] MachineRequest machine, [FromQuery] Guid machineId, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Manager"))
            {
                return Unauthorized();
            }
            Machine updatedMachine = _machineService.UpdateMachine(machine, machineId);
            if (updatedMachine == null)
            {
                return NotFound();
            }
            return Ok(updatedMachine);
        }
        [HttpDelete]
        [Route("Machine")]
        public ActionResult DeleteMachine([FromQuery] Guid machineId, [FromHeader] string token)
        {
            if (!_authService.UserTokenValid(token, "Manager"))
            {
                return Unauthorized();
            }
            _machineService.DeleteMachine(machineId);
            return Ok();
        }
    }
}
