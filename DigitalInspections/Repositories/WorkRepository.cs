using DigitalInspectionsWebApi.Helpers;
using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;

namespace DigitalInspectionsWebApi.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private readonly DigitalInspectionsDbContext _dbContext;
        public WorkRepository(DigitalInspectionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Work CreateWork(Work work)
        {
            _dbContext.Works.Add(work);
            _dbContext.SaveChanges();
            return work;
        }

        public void DeleteWork(Guid workId)
        {
            Work work = GetWork(workId);
            if (work == null)
            {
                return;
            }
            _dbContext.Works.Remove(work);
            _dbContext.SaveChanges();
        }

        public Work GetWork(Guid workId)
        {
            return _dbContext.Works.Find(workId);
        }

        public List<Work> GetWorks(Guid machineId, int limit = 10, int offset = 0)
        {
            return _dbContext.Works.Where(w => w.MachineId == machineId).Skip(offset).Take(limit).ToList();
        }

        public Work UpdateWork(Work work)
        {
            _dbContext.Works.Update(work);
            _dbContext.SaveChanges();
            return work;
        }
    }
}
