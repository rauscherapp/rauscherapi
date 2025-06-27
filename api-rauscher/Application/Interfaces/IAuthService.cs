
using APIs.Security.JWT;
using Application.ViewModels;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IAuthService
  {
    Task<(bool IsValid, TokenViewModel Token)> Register(UserRequest model);
    Task<(bool IsValid, string Token)> Login(UserRequest model);
    Task<(bool IsValid, TokenViewModel Token)> AppLogin(UserRequest model);
    Task<bool> CheckSubscription(UserRequest model);
    Task<bool> SuccesfullSubscriptionUserUpdate(string email);
    Task<bool> CancelledSubscriptionUserUpdate(string customerId);
    Task<bool> DeleteAccount(string email);
  }
}