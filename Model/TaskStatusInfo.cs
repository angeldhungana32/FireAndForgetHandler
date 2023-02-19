using System.ComponentModel.DataAnnotations;

namespace FireAndForgetHandler.Model
{
    public class TaskStatusInfo
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime CompletedTime { get; set; }

        public TaskStatus Status { get; set; }
    }
}
