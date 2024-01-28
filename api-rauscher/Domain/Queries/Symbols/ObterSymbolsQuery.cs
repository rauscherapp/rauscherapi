using Domain.Models;
using MediatR;
using System;

namespace Domain.Queries
{
  public class ObterSymbolsQuery : IRequest<Symbols>
  {
    public Guid ID { get; internal set; }

    public ObterSymbolsQuery(Guid id)
    {
      ID = id;
    }
  }
}
