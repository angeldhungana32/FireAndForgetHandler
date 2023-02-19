using FireAndForgetHandler.Model;
using FireAndForgetHandler.Services;

namespace FireAndForgetHandler.Handler
{
    public class FireAndForgetTaskHandler : IFireAndForgetTaskHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<FireAndForgetTaskHandler> _logger;

        public FireAndForgetTaskHandler(IServiceScopeFactory serviceScopeFactory, 
            ILogger<FireAndForgetTaskHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public void ExecuteAsync<T>(Func<T, Task> taskOperation,
            TaskStatusInfo taskStatus,
            Action<Exception>? exceptionHandler = null)
        {
            Task.Run(async () => 
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<T>();

                    await taskOperation(service)
                        .ContinueWith(async t =>
                        {
                            await UpdateDatabase(t, taskStatus);
                        });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Fire And Forget Failed: {0}",
                        ex.ToString());
                }
            });
        }

        private async Task UpdateDatabase(Task t, TaskStatusInfo taskStatus)
        {
            switch (t.Status)
            {
                case TaskStatus.Faulted:
                    _logger.LogError("Fire and Forget Execution Faulted");
                    break;
                default:
                    _logger.LogInformation($"Fire and Forget for Task {taskStatus.Id} : {t.Status}");
                    break;
            }

            if(t.Status == TaskStatus.RanToCompletion)
            {
                taskStatus.CompletedTime = DateTime.Now;
            }

            taskStatus.Status = t.Status;

            using var scope = _serviceScopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<StatusService>();
            await service.UpdateTaskStatus(taskStatus);
        }
    }
}
