using Domain.ProjectManagement;

namespace Persistance
{
    public static class DataContextExtensions
    {
        public static void EnsureSeedData(this DataContext dbContext)
        {
            dbContext.BacklogItemStatuses.Add(BacklogItemStatus.New);
            dbContext.BacklogItemStatuses.Add(BacklogItemStatus.Active);
            dbContext.BacklogItemStatuses.Add(BacklogItemStatus.Closed);

            dbContext.TaskStatuses.Add(TaskStatus.New);
            dbContext.TaskStatuses.Add(TaskStatus.InProgress);
            dbContext.TaskStatuses.Add(TaskStatus.Done);

            dbContext.SaveChanges();
        }
    }
}