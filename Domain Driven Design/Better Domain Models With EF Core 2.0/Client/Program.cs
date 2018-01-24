using Client.BacklogItemQueries;
using Dapper;
using Domain.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Client
{
    public class Program
    {
        private static IServiceProvider serviceProvider;
        private static string connectionString;

        public static void Main(string[] args)
        {
            AsyncMain(args).GetAwaiter().GetResult();
        }

        private static async System.Threading.Tasks.Task AsyncMain(string[] args)
        {
            InitApp();
            DropDbIfExists();
            InitDatabase();

            await CreateNewUser();
            var backlogItemId = await CreateNewBacklog();
            await AddTaskToBacklog(backlogItemId);
            await MakeBacklogActive(backlogItemId);
            DisplayBacklogsWithTasks();

            Console.ReadKey();
        }

        private static void InitApp()
        {
            Console.WriteLine("Initializing application...");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options => options.UseSqlServer(connectionString))
                .AddSingleton<IConfigurationRoot>(configuration)
                .AddScoped<IBacklogItemRepository, BacklogItemRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAllBacklogsQuery, AllBacklogsQuery>()
                .BuildServiceProvider();

            Console.WriteLine("Done.\n");
        }

        private static void DropDbIfExists()
        {
            Console.WriteLine("Removing database if exists...");

            var dbContext = serviceProvider.GetService<DataContext>();
            dbContext.Database.EnsureDeleted();

            Console.WriteLine("Done.\n");
        }
        
        private static void InitDatabase()
        {
            Console.WriteLine("Initializing database...");

            var dbContext = serviceProvider.GetService<DataContext>();
            dbContext.Database.EnsureCreated();
            dbContext.EnsureSeedData();

            Console.WriteLine("Done.\n");
        }

        private static async System.Threading.Tasks.Task CreateNewUser()
        {
            Console.WriteLine("Creating new user...");

            var user = User.New("Jan Kowalski", "j.kowalski@email.com");
            user.SetAddress(Address.New("Długa 23/4", "Warszawa", "Mazowieckie", "Polska", "00-001"));

            var repo = serviceProvider.GetService<IUserRepository>();
            await repo.SaveAsync(user);

            Console.WriteLine("Done.\n");
        }

        private static async System.Threading.Tasks.Task<int> CreateNewBacklog()
        {
            Console.WriteLine("Creating new backlog...");

            var userRepository = serviceProvider.GetService<IUserRepository>();
            var backlogRepository = serviceProvider.GetService<IBacklogItemRepository>();

            var user = await userRepository.FindAsync("j.kowalski@email.com");

            var backlogItem = BacklogItem.New("Payments module", "This backlog contains tasks regarding new payments functionality");
            backlogItem.SetOwner(user.Id);

            await backlogRepository.SaveAsync(backlogItem);

            Console.WriteLine("Done.\n");

            return backlogItem.Id;
        }

        private static async System.Threading.Tasks.Task AddTaskToBacklog(int backlogItemId)
        {
            Console.WriteLine("Adding task to backlog...");

            var repo = serviceProvider.GetService<IBacklogItemRepository>();

            var task = Task.New("Create endpoint to handle incomming payment requests");
            
            var backlogItem = await repo.FindAsync(backlogItemId);
            if (backlogItem == null)
                throw new BacklogItemException($"No backlog with id: {backlogItemId}");

            backlogItem.AddTask(task);

            await repo.SaveAsync(backlogItem);

            Console.WriteLine("Done.\n");
        }

        private static async System.Threading.Tasks.Task MakeBacklogActive(int backlogItemId)
        {
            Console.WriteLine("Making backlog item active...");

            var repo = serviceProvider.GetService<IBacklogItemRepository>();

            var backlogItem = await repo.FindAsync(backlogItemId);
            if (backlogItem == null)
                throw new BacklogItemException($"No backlog with id: {backlogItemId}");

            backlogItem.MakeActive();

            await repo.SaveAsync(backlogItem);

            Console.WriteLine("Done.\n");
        }

        private static void DisplayBacklogsWithTasks()
        {
            var query = serviceProvider.GetService<IAllBacklogsQuery>();
            var backlogs = query.Execute();
            backlogs
                .AsList()
                 .ForEach(item =>
                 {
                     Console.WriteLine($"{item.Name} - {item.Description}");
                     item.Tasks.ForEach(t =>
                     {
                         Console.WriteLine($" {t.Id}: {t.Name}");
                     });
                 });   
        }
    }
}