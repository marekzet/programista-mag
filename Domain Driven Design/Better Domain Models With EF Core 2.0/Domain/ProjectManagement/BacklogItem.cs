using Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Domain.ProjectManagement
{
    public class BacklogItem : Entity, IAggregateRoot
    {
        private string name;
        private string description;
        private int? ownerId;
        private int statusId;
        private DateTime createdOn;
        private DateTime? closedOn;
        private readonly List<Task> tasks;

        public int Id { get; private set; }
        public IReadOnlyCollection<Task> Tasks => tasks;

        // Required by EF
        protected BacklogItem()
        {
            tasks = new List<Task>();
        }

        protected BacklogItem(string name, string description = null)
        {
            this.name = name;
            this.description = description;
            createdOn = DateTime.Now;
            statusId = BacklogItemStatus.New.Id;
            tasks = new List<Task>();
        }

        public static BacklogItem New(string name, string description = null) =>
            new BacklogItem(name, description);

        public void SetOwner(int ownerId)
        {
            this.ownerId = ownerId;
        }

        public void MakeActive()
        {
            if (statusId == BacklogItemStatus.Archived.Id)
                throw new DomainException("Cannot set archived backlog to active.");

            statusId = BacklogItemStatus.Active.Id;
        }

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        public void Close()
        {
            closedOn = DateTime.Now;
            statusId = BacklogItemStatus.Closed.Id;
        }
    }
}