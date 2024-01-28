using System;

namespace Domain.QueryParameters
{
    public class QueryParameters
    {
        const int maxPageSize = 50;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; } = "ID";
        public string Fields { get; set; }

        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

    }
}
