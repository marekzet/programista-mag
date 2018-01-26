namespace Domain.ProjectManagement
{
    public interface IBacklogItemRepository
    {
        System.Threading.Tasks.Task<BacklogItem> FindAsync(int id);
        System.Threading.Tasks.Task SaveAsync(BacklogItem backlogItem);
    }
}