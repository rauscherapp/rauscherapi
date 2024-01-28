using Application.Interfaces;
using Domain.QueryParameters;

namespace Application.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(PagedList<T> exports, QueryParameters exportParameters, string routName, IUriAppService uriService)
        {
            var previousPageLink = exports.HasPrevious ?
                uriService.CreateExportsResourceUri(exportParameters,
                ResourceUriType.PreviousPage,
                routName) : null;

            var nextPageLink = exports.HasNext ?
                uriService.CreateExportsResourceUri(exportParameters,
                ResourceUriType.NextPage,
                routName) : null;

            return new PagedResponse<T>()
            {
                Data = exports,
                PaginationMetadata = new PaginationMetadata(exports.TotalCount, exports.PageSize, exports.CurrentPage, exports.TotalPages, previousPageLink, nextPageLink),
                PageLinks = new PageLinks(previousPageLink, nextPageLink)
            };
        }
    }
}
