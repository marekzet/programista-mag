using Domain.SharedKernel;

namespace Domain.ProjectManagement
{
    public class BacklogItemStatus : Enumeration
    {
        public static BacklogItemStatus New = new BacklogItemStatus(1, "New");
        public static BacklogItemStatus Active = new BacklogItemStatus(2, "Active");
        public static BacklogItemStatus Closed = new BacklogItemStatus(3, "Closed");
        public static BacklogItemStatus Archived = new BacklogItemStatus(4, "Archived");

        protected BacklogItemStatus()
        {
        }

        public BacklogItemStatus(int id, string name) : base(id, name)
        {
        }
    }
}