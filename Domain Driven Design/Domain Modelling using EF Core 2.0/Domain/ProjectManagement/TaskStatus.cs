using Domain.SharedKernel;
using System.Collections.Generic;
using System.Linq;

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

        internal static IEnumerable<TaskStatus> List() =>
            new[] { New, InProgress, Done };

        internal static TaskStatus From(int id)
        {
            var status = List().SingleOrDefault(s => s.Id == id);
            if (status == null)
                throw new TaskException("Invalid value of TaskStatus.");

            return status;
        }
    }
}