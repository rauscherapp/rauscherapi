using APIs.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
  public class TokenViewModel
  {
    public bool Authenticated { get; set; }
    public string? Created { get; set; }
    public string? Expiration { get; set; }
    public string? AccessToken { get; set; }
    public string? Message { get; set; }
    public UserResponse? User { get; set; }
    public AppParametersViewModel AppParameters { get; set; }
  }
}
