
namespace Application.Helpers
{
    public class PaginationMetadata
    {
        public int TotalCount { get; }
        public int PageSize { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public string PreviousPageLink { get; }
        public string NextPageLink { get; }

        public PaginationMetadata(int totalCount, int pageSize, int currentPage, int totalPages, string previousPageLink, string nextPageLink)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PreviousPageLink = previousPageLink;
            NextPageLink = nextPageLink;
        }
    }
}
