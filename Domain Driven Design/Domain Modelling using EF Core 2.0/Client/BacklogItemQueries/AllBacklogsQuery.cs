using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Client.BacklogItemQueries
{
    internal class AllBacklogsQuery : IAllBacklogsQuery
    {
        private readonly IConfigurationRoot configuration;

        public AllBacklogsQuery(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
        }

        public IReadOnlyCollection<BacklogItemViewModel> Execute()
        {
            var lookup = new Dictionary<int, BacklogItemViewModel>();
            var connString = configuration.GetConnectionString("DefaultConnection");
            using (var conn = new SqlConnection(connString))
            {
                conn.Query<BacklogItemViewModel, TaskViewModel, BacklogItemViewModel>(@"
                    SELECT
                        b.Id, b.Name, b.Description, t.Id, t.Name
                    FROM BacklogItem b
                    JOIN Task t ON b.Id = t.BacklogItemId",
                        (b, t) =>
                        {
                            BacklogItemViewModel backlog;
                            if (!lookup.TryGetValue(b.Id, out backlog))
                                lookup.Add(b.Id, backlog = b);

                            if (backlog.Tasks == null)
                                backlog.Tasks = new List<TaskViewModel>();

                            backlog.Tasks.Add(t);
                            return backlog;
                        });
            }

            return lookup.Values.AsList();
        }
    }
}