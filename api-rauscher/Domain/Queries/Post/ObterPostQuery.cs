using Domain.Models;
using MediatR;
using System;

namespace Domain.Queries
{
  public class ObterPostQuery : IRequest<Post>
  {
    public Guid Id { get; internal set; }

    public ObterPostQuery(Guid id)
    {
      Id = id;
    }
  }
}
