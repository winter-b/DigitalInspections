using DigitalInspectionsWebApi.Interfaces;
using DigitalInspectionsWebApi.Models;
using DigitalInspectionsWebApi.Models.Requests;

namespace DigitalInspectionsWebApi.Services
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _workRepository;

        public WorkService(IWorkRepository workRepository)
        {
            _workRepository = workRepository;
        }

        public Work CreateWork(WorkRequest workRequest, string addedBy)
        {
            Work work = new Work
            {
                Id = Guid.NewGuid(),
                MachineId = workRequest.MachineId,
                AddedBy = addedBy,
                Description = workRequest.Description ?? "",
                Name = workRequest.Name,
                Category = workRequest.Category ?? "",
                Added = DateTimeOffset.UtcNow,
                LastUpdated = DateTimeOffset.UtcNow,
                NextInspection = workRequest.NextInspection ?? DateTimeOffset.MaxValue,
                IsInspected = false

            };
            return _workRepository.CreateWork(work);
        }

        public void DeleteWork(Guid workId)
        {
            _workRepository.DeleteWork(workId);
        }

        public Work GetWork(Guid workId)
        {
            return _workRepository.GetWork(workId);
        }

        public List<Work> GetWorks(Guid machineId, int limit, int offset)
        {
            try
            {
                return _workRepository.GetWorks(machineId, limit, offset);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Work>();
            }
        }

        public Work UpdateWork(WorkRequest workRequest, Guid workId)
        {
            Work work = GetWork(workId);
            if (work == null)
            {
                return null;
            }
            work.Name = workRequest.Name ?? work.Name;
            work.Description = workRequest.Description ?? work.Description;
            work.Category = workRequest.Category ?? work.Category;
            work.NextInspection = workRequest.NextInspection ?? work.NextInspection;
            work.LastUpdated = DateTimeOffset.UtcNow;
            work.IsInspected = workRequest.IsInspected ?? work.IsInspected;
            return _workRepository.UpdateWork(work);
        }
    }
}
