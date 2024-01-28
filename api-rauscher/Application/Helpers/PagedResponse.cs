using Domain.QueryParameters;
using System.Collections.Generic;

namespace Application.Helpers
{
    public class PagedResponse<T> : List<T>
    {
        public PagedList<T> Data { get; set; }
        public PaginationMetadata PaginationMetadata { get; set; }
        public PageLinks PageLinks { get; set; }
        public PagedResponse() { }

        public PagedResponse(PagedList<T> data, string previousPageLink, string nextPageLink)
        {
            Data = data;
            PaginationMetadata = new PaginationMetadata(data.TotalCount, data.PageSize, data.CurrentPage, data.TotalPages, previousPageLink, nextPageLink);
            PageLinks = new PageLinks(previousPageLink, nextPageLink);
        }
    }
}
