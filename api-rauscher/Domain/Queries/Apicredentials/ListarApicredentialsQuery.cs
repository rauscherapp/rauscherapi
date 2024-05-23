using Domain.Models;
using Domain.QueryParameters;
using MediatR;

namespace Domain.Queries
{
  public class ListarApiCredentialsQuery : IRequest<PagedList<ApiCredentials>>
  {
    public ApiCredentialsParameters Parameters { get; internal set; }

    public ListarApiCredentialsQuery(ApiCredentialsParameters parameters)
    {
      Parameters = parameters;
    }
  }
}
