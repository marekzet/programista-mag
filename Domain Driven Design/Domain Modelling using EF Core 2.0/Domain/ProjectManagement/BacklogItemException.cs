using System;

namespace Domain.ProjectManagement
{
    public class BacklogItemException : Exception
    {
        public BacklogItemException(string message) : base(message)
        {
        }
    }
}