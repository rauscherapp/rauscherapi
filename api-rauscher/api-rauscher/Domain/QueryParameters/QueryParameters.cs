using System;

namespace Domain.QueryParameters
{
    public class QueryParameters
    {
        public string Vpe { get; set; }

        const int maxPageSize = 50;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; } = "Vpe";
        public string Fields { get; set; }
        public int? Sacas { get; set; }
        public DateTime? DataVpe { get; set; }
        public DateTime? DataEmbarque { get; set; }

        private int _pageSize = 20;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

    }
}
