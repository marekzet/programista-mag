using System;

namespace Domain.ProjectManagement
{
    public class TaskException : Exception
    {
        public TaskException(string message) : base(message)
        {
        }
    }
}