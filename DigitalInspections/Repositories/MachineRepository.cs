using DigitalInspectionsWebApi.Helpers;
using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;

namespace DigitalInspectionsWebApi.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly DigitalInspectionsDbContext _dbContext;
        public MachineRepository(DigitalInspectionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Machine CreateMachine(Machine newMachine)
        {
            _dbContext.Machines.Add(newMachine);
            _dbContext.SaveChanges();
            return newMachine;
        }

        public void DeleteMachine(Guid machineId)
        {
            _dbContext.Machines.Remove(GetMachine(machineId));
            _dbContext.SaveChanges();
        }

        public Machine GetMachine(Guid machineId)
        {
            return _dbContext.Machines.Find(machineId);
        }

        public List<Machine> GetMachines(int limit = 10, int offset = 0)
        {
            return _dbContext.Machines.Skip(offset).Take(limit).ToList();
        }

        public Machine UpdateMachine(Machine machineToUpdate)
        {
            _dbContext.Machines.Update(machineToUpdate);
            _dbContext.SaveChanges();
            return machineToUpdate;
        }
    }
}
