using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using IdentityAuth = Microsoft.AspNetCore.Identity;

namespace XPros_Stock_And_Inventory.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        #region Fields
        private IUser<Guid> _user;
        #endregion

        #region Properties
        [BindProperty]
        public UserInput Input { get; set; }
        #endregion

        public LoginModel(IUser<Guid> user, IUserInfo<Guid> userInfo)
        {
            _user = user;
            Input = (UserInput)userInfo;
        }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl ??= Url.Content("~/");
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            _user.SetInfo(Input);
            IdentityAuth.SignInResult result = _user.SignIn(Input.Password);
            if (result.Succeeded)
                return LocalRedirect(returnUrl);

            if (result.IsLockedOut)
                return RedirectToPage("./Lockout");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }            
        }
    }
}
