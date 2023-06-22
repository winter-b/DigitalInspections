using DigitalInspectionsWebApi.Models;

namespace DigitalInspectionsWebApi.Interfaces
{
    public interface IWorkRepository
    {
        public Work CreateWork(Work work);
        public void DeleteWork(Guid workId);
        public Work GetWork(Guid workId);
        public List<Work> GetWorks(Guid machineId, int limit, int offset);
        public Work UpdateWork(Work newWork);

    }
}
