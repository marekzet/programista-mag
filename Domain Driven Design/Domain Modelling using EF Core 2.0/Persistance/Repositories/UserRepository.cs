using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async System.Threading.Tasks.Task<User> FindAsync(string email)
        {
            return await dataContext.Users.SingleOrDefaultAsync(u =>
                EF.Property<string>(u, "email") == email);
        }

        public async System.Threading.Tasks.Task SaveAsync(User user)
        {
            dataContext.Users.Add(user);
            await dataContext.SaveChangesAsync();
        }
    }
}