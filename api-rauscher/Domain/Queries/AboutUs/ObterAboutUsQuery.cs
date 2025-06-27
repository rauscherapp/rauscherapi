using Domain.Models;
using MediatR;

namespace Domain.Queries
{
  public class ObterAboutUsQuery : IRequest<AboutUs>
  {
    public ObterAboutUsQuery()
    {      
    }
  }
}
