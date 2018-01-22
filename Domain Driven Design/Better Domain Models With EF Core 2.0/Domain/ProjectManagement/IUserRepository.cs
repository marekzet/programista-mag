namespace Domain.ProjectManagement
{
    public interface IUserRepository
    {
        System.Threading.Tasks.Task<User> FindAsync(string email);
        System.Threading.Tasks.Task SaveAsync(User user);
    }
}