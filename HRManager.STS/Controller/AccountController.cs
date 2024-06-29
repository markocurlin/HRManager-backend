using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HRManager.STS.Models;
using Microsoft.AspNetCore.Authorization;
using IdentityServerHost.Quickstart.UI;
using HRManager.STS.Data;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Extensions;

namespace HRManager.STS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var userIdentity = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Role = registerModel.Role,
            };

            var result = await _userManager.CreateAsync(userIdentity, registerModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            var user = new UserProfile
            {
                Id = userIdentity.Id,
                UserName = userIdentity.UserName,
                Email = userIdentity.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Role = userIdentity.Role,
            };

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(string id, EditModel editModel)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            
            user.Email = editModel.Email;
            user.UserName = editModel.UserName;
            user.Role = editModel.Role;
           
            var result = await _userManager.UpdateAsync(user);
            
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (!editModel.Password.IsNullOrEmpty())
            {
                result = await _userManager.ChangePasswordAsync(user, editModel.Password, editModel.NewPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return NoContent();
        }
    }
}
