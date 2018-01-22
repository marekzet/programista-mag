using Domain.SharedKernel;

namespace Domain.ProjectManagement
{
    public class Task : Entity
    {
        private string name;
        private string description;
        private int statusId;

        public int Id { get; private set; }

        // Required by EF
        protected Task()
        {
        }

        protected Task(string name, string description = null)
        {
            this.name = name;
            this.description = description;
            statusId = TaskStatus.New.Id;
        }

        public static Task New(string name, string description = null) =>
            new Task(name, description);

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Invalid task name.");

            this.name = name;
        }

        public void UpdateDescription(string description)
        {
            this.description = description;
        }

        public void ChangeStatus(TaskStatus status)
        {
            statusId = status.Id;
        }
    }
}