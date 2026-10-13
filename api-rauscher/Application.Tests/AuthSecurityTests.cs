using System;
using System.Threading.Tasks;
using APIs.Security.JWT;
using Application.Services;
using AutoMapper;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class AuthSecurityTests
    {
        private static Mock<UserManager<ApplicationUser>> CreateUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private static AccessManager CreateAccessManager(UserManager<ApplicationUser> userManager)
        {
            var key = Convert.ToBase64String(new byte[32]);
            return new AccessManager(userManager, new SigningConfigurations(key), new TokenConfigurations { Seconds = 3600 });
        }

        private static AuthService CreateAuthService(IAccessManager accessManager, UserManager<ApplicationUser> userManager)
        {
            var options = new Mock<IOptionsSnapshot<ParametersOptions>>();
            options.Setup(o => o.Value).Returns(new ParametersOptions());
            return new AuthService(accessManager, options.Object, userManager, Mock.Of<IMapper>(), NullLogger<AuthService>.Instance);
        }

        [Fact]
        public async Task CreateUser_ExistingEmail_DoesNotDeleteOrRecreateTheAccount()
        {
            var userManager = CreateUserManager();
            var existing = new ApplicationUser { Email = "cliente@exemplo.com", UserName = "cliente@exemplo.com" };
            userManager.Setup(u => u.FindByEmailAsync("cliente@exemplo.com")).ReturnsAsync(existing);

            var created = await CreateAccessManager(userManager.Object)
                .CreateUser(new UserRequest { Email = "cliente@exemplo.com", Password = "OutraSenha#1" });

            Assert.False(created);
            userManager.Verify(u => u.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Never);
            userManager.Verify(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CreateUser_NewEmail_CreatesUserWithAccessRole()
        {
            var userManager = CreateUserManager();
            userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            userManager.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var created = await CreateAccessManager(userManager.Object)
                .CreateUser(new UserRequest { Email = "novo@exemplo.com", Password = "Senha#123" });

            Assert.True(created);
            userManager.Verify(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), Roles.ROLE_ACESSO_APIS), Times.Once);
        }

        [Fact]
        public async Task Register_ExistingEmail_ReturnsInvalidWithoutToken()
        {
            var accessManager = new Mock<IAccessManager>();
            accessManager.Setup(a => a.CreateUser(It.IsAny<UserRequest>())).ReturnsAsync(false);
            var service = CreateAuthService(accessManager.Object, CreateUserManager().Object);

            var (isValid, token) = await service.Register(new UserRequest { Email = "cliente@exemplo.com", Password = "x" });

            Assert.False(isValid);
            Assert.Null(token);
            accessManager.Verify(a => a.GenerateToken(It.IsAny<UserResponse>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAccount_WrongPassword_DoesNotDelete()
        {
            var userManager = CreateUserManager();
            var accessManager = new Mock<IAccessManager>();
            accessManager.Setup(a => a.ValidateCredentials(It.IsAny<UserRequest>())).ReturnsAsync((new UserResponse(), false));
            var service = CreateAuthService(accessManager.Object, userManager.Object);

            var deleted = await service.DeleteAccount(new UserRequest { Email = "cliente@exemplo.com", Password = "errada" });

            Assert.False(deleted);
            userManager.Verify(u => u.DeleteAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAccount_ValidCredentials_DeletesOnlyThatUser()
        {
            var userManager = CreateUserManager();
            var user = new ApplicationUser { Email = "cliente@exemplo.com", UserName = "cliente@exemplo.com" };
            userManager.Setup(u => u.FindByEmailAsync("cliente@exemplo.com")).ReturnsAsync(user);
            userManager.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);
            var accessManager = new Mock<IAccessManager>();
            accessManager.Setup(a => a.ValidateCredentials(It.IsAny<UserRequest>())).ReturnsAsync((new UserResponse(), true));
            var service = CreateAuthService(accessManager.Object, userManager.Object);

            var deleted = await service.DeleteAccount(new UserRequest { Email = "cliente@exemplo.com", Password = "Senha#123" });

            Assert.True(deleted);
            userManager.Verify(u => u.DeleteAsync(user), Times.Once);
        }
    }
}
