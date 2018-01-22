using Domain.SharedKernel;

namespace Domain.ProjectManagement
{
    public class TaskStatus : Enumeration
    {
        public static TaskStatus New = new TaskStatus(1, "New");
        public static TaskStatus InProgress = new TaskStatus(2, "In progress");
        public static TaskStatus Done = new TaskStatus(3, "Done");

        protected TaskStatus()
        {
        }

        public TaskStatus(int id, string name) : base(id, name)
        {
        }
    }
}