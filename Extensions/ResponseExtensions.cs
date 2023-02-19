using FireAndForgetHandler.Model;
using FireAndForgetHandler.Model.Dto;

namespace FireAndForgetHandler.Extensions
{
    public static class ResponseExtensions
    {
        public static TaskStatusInfoResponse? Response(this TaskStatusInfo statusInfo)
        {
            if (statusInfo == null)
                return null;

            return new TaskStatusInfoResponse()
            {
                Status = statusInfo.Status,
                CompletedTime = statusInfo.CompletedTime,
                CreatedTime = statusInfo.CreatedTime,
                Id = statusInfo.Id.ToString()
            };
        }
    }
}
