using Application.Helpers;
using Domain.QueryParameters;

namespace Application.Interfaces
{
  public interface IUriAppService
  {
    string CreateExportsResourceUri(QueryParameters parameters, ResourceUriType type, string routName);
  }
}
