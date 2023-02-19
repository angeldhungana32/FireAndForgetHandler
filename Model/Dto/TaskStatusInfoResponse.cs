using System.ComponentModel.DataAnnotations;

namespace FireAndForgetHandler.Model.Dto
{
    public class TaskStatusInfoResponse
    {
        public string? Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime CompletedTime { get; set; }

        public TaskStatus Status { get; set; }
    }
}
