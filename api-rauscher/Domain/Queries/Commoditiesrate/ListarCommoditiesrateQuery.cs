using Domain.Models;
using Domain.QueryParameters;
using MediatR;

namespace Domain.Queries
{
  public class ListarCommoditiesRateQuery : IRequest<PagedList<CommoditiesRate>>
  {
    public CommoditiesRateParameters Parameters { get; internal set; }

    public ListarCommoditiesRateQuery(CommoditiesRateParameters parameters)
    {
      Parameters = parameters;
    }
  }
}
