using Domain.QueryParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IEmailService
  {
    Task<bool> SendEmailAsync(AppEmailParameters parameters);
  }
}
