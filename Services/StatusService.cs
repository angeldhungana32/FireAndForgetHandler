using FireAndForgetHandler.Extensions;
using FireAndForgetHandler.Model;
using FireAndForgetHandler.Model.Dto;
using FireAndForgetHandler.Repository;

namespace FireAndForgetHandler.Services
{
    public class StatusService
    {
        private readonly IRepository<TaskStatusInfo> _statusRepository;

        public StatusService(IRepository<TaskStatusInfo> statusRepository) 
        {
            _statusRepository = statusRepository;
        }

        public async Task<TaskStatusInfoResponse?> GetStatusById(string id)
        {
            if(Guid.TryParse(id, out var statusId))
            {
                TaskStatusInfo statusInfo = await _statusRepository
                    .GetByIdAsync(statusId);

                return statusInfo.Response();
            }

            return default;
        }

        public async Task<TaskStatusInfo> CreateTaskStatus()
        {
            TaskStatusInfo statusInfo = new ()
            {
                CreatedTime = DateTime.UtcNow,
                Status = TaskStatus.Created
            };

            statusInfo = await _statusRepository.AddAsync(statusInfo);

            return statusInfo;
        }

        public async Task UpdateTaskStatus(TaskStatusInfo statusInfo)
        {
            await _statusRepository.UpdateAsync(statusInfo);
        }
    }
}
