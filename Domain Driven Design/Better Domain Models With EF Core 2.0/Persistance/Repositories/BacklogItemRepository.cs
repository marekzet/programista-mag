using Domain.ProjectManagement;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BacklogItemRepository : IBacklogItemRepository
    {
        private readonly DataContext dbContext;

        public BacklogItemRepository(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BacklogItem> FindAsync(int id)
        {
            return await dbContext.BacklogItems.FindAsync(id);
        }

        public async System.Threading.Tasks.Task SaveAsync(BacklogItem backlogItem)
        {
            if (backlogItem.Id != 0)
                dbContext.BacklogItems.Update(backlogItem);
            else
                dbContext.BacklogItems.Add(backlogItem);

            await dbContext.SaveChangesAsync();
        }
    }
}