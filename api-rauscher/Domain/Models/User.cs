using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Domain.Models
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public User(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => GetName();
        public string Email => GetEmail();

        private string GetName()
        {
            return _accessor.HttpContext.User.Identity.Name;
        }

        private string GetEmail()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
