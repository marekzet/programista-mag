using System;

namespace Domain.ProjectManagement
{
    public class BacklogItemNotFoundException : Exception
    {
        public BacklogItemNotFoundException(string message) : base(message)
        {
        }
    }
}