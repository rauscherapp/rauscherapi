using Application.Helpers;
using Application.Interfaces;
using Domain.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class UriAppService : IUriAppService
    {
        private readonly IUrlHelper _url;

        public UriAppService(IUrlHelper url)
        {
            _url = url;
        }

        public string CreateExportsResourceUri(QueryParameters parameters, ResourceUriType type, string routName)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _url.Link(routName,
                      new
                      {
                          fields = parameters.Fields,
                          orderBy = parameters.OrderBy,
                          pageNumber = parameters.PageNumber - 1,
                          pageSize = parameters.PageSize,
                          searchQuery = parameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return _url.Link(routName,
                      new
                      {
                          fields = parameters.Fields,
                          orderBy = parameters.OrderBy,
                          pageNumber = parameters.PageNumber + 1,
                          pageSize = parameters.PageSize,
                          searchQuery = parameters.SearchQuery
                      });

                default:
                    return _url.Link(routName,
                    new
                    {
                        fields = parameters.Fields,
                        orderBy = parameters.OrderBy,
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            }
        }
    }
}
