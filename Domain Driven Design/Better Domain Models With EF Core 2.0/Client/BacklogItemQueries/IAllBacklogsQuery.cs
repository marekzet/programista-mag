using System.Collections.Generic;

namespace Client.BacklogItemQueries
{
    internal interface IAllBacklogsQuery
    {
        IReadOnlyCollection<BacklogItemViewModel> Execute();
    }
}