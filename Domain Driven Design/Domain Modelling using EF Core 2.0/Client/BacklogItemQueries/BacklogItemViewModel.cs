using System.Collections.Generic;

namespace Client.BacklogItemQueries
{
    internal class BacklogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
    }
}