using System;

namespace Domain.ProjectManagement
{
    public class AddressException : Exception
    {
        public AddressException(string message) : base(message)
        {
        }
    }
}