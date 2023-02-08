using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Services.Contract;

namespace WebAdvert.Web.Services
{
    public class AccountService : IAccountService
    {
        private CognitoUserPool _userPool;
        private ILogger<AccountService> _logger;
        private UserManager<CognitoUser> _userManager;
        private SignInManager<CognitoUser> _signInManager;

        public AccountService(
            CognitoUserPool userPool,
            ILogger<AccountService> logger,
            UserManager<CognitoUser> userManager,
            SignInManager<CognitoUser> signInManager
        )
        {
            _logger = logger;
            _userPool = userPool;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<CognitoUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> ConfirmUserAsync(CognitoUser user, SignupConfirmModel model)
        {
            return await _userManager.ConfirmEmailAsync(user, model.Code);
        }

        public async Task<IdentityResult> CreateUserAsync(CognitoUser user, SignupModel model)
        {
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<IdentityResult> LoginAsync(LoginModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
