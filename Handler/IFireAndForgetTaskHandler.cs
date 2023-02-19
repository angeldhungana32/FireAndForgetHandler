using FireAndForgetHandler.Model;

namespace FireAndForgetHandler.Handler
{
    public interface IFireAndForgetTaskHandler
    {
        void ExecuteAsync<T>(Func<T, Task> taskOperation,
            TaskStatusInfo taskStatus,
            Action<Exception>? exceptionHandler = null);
    }
}
