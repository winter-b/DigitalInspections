using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;
using DigitalInspectionsWebApi.Models.Requests;

namespace DigitalInspectionsWebApi.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public Machine CreateMachine(MachineRequest machine)
        {
            Machine newMachine = new Machine
            {
                Id = Guid.NewGuid(),
                Name = machine.Name,
                Category = machine.Category,
                Description = machine.Description ?? "",
                LastUpdated = DateTimeOffset.UtcNow
            };
            return _machineRepository.CreateMachine(newMachine);
        }

        public void DeleteMachine(Guid machineId)
        {
            _machineRepository.DeleteMachine(machineId);
        }

        public Machine GetMachine(Guid machineId)
        {
            return _machineRepository.GetMachine(machineId);
        }

        public List<Machine> GetMachines(int limit, int offset)
        {
            return _machineRepository.GetMachines(limit, offset);
        }

        public Machine UpdateMachine(MachineRequest machine, Guid machineId)
        {
            Machine machineToUpdate = GetMachine(machineId);
            if (machineToUpdate == null)
            {
                return null;
            }
            machineToUpdate.Name = machine.Name ?? machineToUpdate.Name;
            machineToUpdate.Category = machine.Category ?? machineToUpdate.Category;
            machineToUpdate.Description = machine.Description ?? machineToUpdate.Description;
            machineToUpdate.LastUpdated = DateTimeOffset.UtcNow;
            return _machineRepository.UpdateMachine(machineToUpdate);
        }
    }
}
