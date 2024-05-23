
using APIs.Security.JWT;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IAuthService
  {
    Task<bool> Register(UserRequest model);
    Task<(bool IsValid, string Token)> Login(UserRequest model);
  }
}