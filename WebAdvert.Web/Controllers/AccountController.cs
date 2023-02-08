using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Services.Contract;

namespace WebAdvert.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                CognitoUser user = await _accountService.GetUserByEmail(model.Email);

                if (user != null && user.Status != null)
                {
                    return Ok(new
                    {
                        Success = false,
                        Message = "User with this email already exists"
                    });
                }

                var result = await _accountService.CreateUserAsync(user, model);

                if (result != null && result.Succeeded)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "User created successfully"
                    });
                }
                else
                {
                    List<string> errors = new List<string>();

                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }

                    return Ok(new
                    {
                        Success = false,
                        Message = "Cannot create user: " + string.Join('\n', errors)
                    });
                }
            }
            else
            {
                IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors);

                List<string> errors = new List<string>();

                foreach (var error in modelErrors)
                {
                    errors.Add(error.ErrorMessage);
                }

                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid request: " + string.Join('\n', errors)
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmSignup(SignupConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.GetUserByEmail(model.Email);

                if (user != null)
                {
                    var result = await _accountService.ConfirmUserAsync(user, model);

                    if (result != null && result.Succeeded)
                    {
                        return Ok(new
                        {
                            Success = true,
                            Message = "User account confirmed"
                        });
                    }
                    else
                    {
                        List<string> errors = new List<string>();

                        foreach (var error in result.Errors)
                        {
                            errors.Add(error.Description);
                        }

                        return BadRequest(new
                        {
                            Success = false,
                            Message = "Invalid request: " + string.Join('\n', errors)
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        UserExists = "User with this email already exists"
                    });
                }
            }
            else
            {
                IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(v => v.Errors);

                List<string> errors = new List<string>();

                foreach (var error in modelErrors)
                {
                    errors.Add(error.ErrorMessage);
                }

                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid request: " + string.Join('\n', errors)
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _accountService.LoginAsync(model);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Valid login"
                });
            }
            else
            {
                return Ok(new
                {
                    Success = true,
                    Message = "Invalid login"
                });
            }
        }
    }
}