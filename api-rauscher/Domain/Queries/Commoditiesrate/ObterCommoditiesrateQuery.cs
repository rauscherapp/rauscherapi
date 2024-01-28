using Domain.Models;
using MediatR;
using System;

namespace Domain.Queries
{
  public class ObterCommoditiesRateQuery : IRequest<CommoditiesRate>
  {
    public Guid Id { get; internal set; }

    public ObterCommoditiesRateQuery(Guid id)
    {
      Id = id;
    }
  }
}
