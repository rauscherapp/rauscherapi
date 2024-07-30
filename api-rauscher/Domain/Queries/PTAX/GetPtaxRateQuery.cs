using Domain.Models;
using Domain.QueryParameters;
using MediatR;

namespace Domain.Queries
{
  public class GetPtaxRateQuery : IRequest<PagedList<CommoditiesRate>>
  {
    public CommoditiesRateParameters Parameters { get; internal set; }

    public GetPtaxRateQuery(CommoditiesRateParameters parameters)
    {
      Parameters = parameters;
    }
  }
}
