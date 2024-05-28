using Domain.Models;
using Domain.QueryParameters;
using MediatR;

namespace Domain.Queries
{
  public class ListarAppParametersQuery : IRequest<PagedList<AppParameters>>
  {
    public AppParametersParameters Parameters { get; internal set; }

    public ListarAppParametersQuery(AppParametersParameters parameters)
    {
      Parameters = parameters;
    }
  }
}
