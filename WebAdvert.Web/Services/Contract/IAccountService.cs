using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebAdvert.Web.Models;

namespace WebAdvert.Web.Services.Contract
{
    public interface IAccountService
    {
        public Task<CognitoUser> GetUserByEmail(string email);
        public Task<IdentityResult> LoginAsync(LoginModel model);
        public Task<IdentityResult> CreateUserAsync(CognitoUser user, SignupModel model);
        public Task<IdentityResult> ConfirmUserAsync(CognitoUser user, SignupConfirmModel model);
    }
}
