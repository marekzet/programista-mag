using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client.BacklogItemQueries
{
    internal class BacklogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public string StatusText { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
    }
}