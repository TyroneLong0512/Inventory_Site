using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace XPros_Stock_And_Inventory.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        #region Fields
        private IUser<Guid> _user;
        #endregion

        #region Properties
        [BindProperty]
        public UserInput Input { get; set; }
        #endregion

        public ForgotPasswordModel(IUser<Guid> user)
        {
            _user = user;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //ToDo: Implment the forgot password logic into the user class
            //if (ModelState.IsValid)
            //{
            //    var user = await _userManager.FindByEmailAsync(Input.Email);
            //    if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            //    {
            //        // Don't reveal that the user does not exist or is not confirmed
            //        return RedirectToPage("./ForgotPasswordConfirmation");
            //    }

            //    // For more information on how to enable account confirmation and password reset please
            //    // visit https://go.microsoft.com/fwlink/?LinkID=532713
            //    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //    var callbackUrl = Url.Page(
            //        "/Account/ResetPassword",
            //        pageHandler: null,
            //        values: new { area = "Identity", code },
            //        protocol: Request.Scheme);

            //    await _emailSender.SendEmailAsync(
            //        Input.Email,
            //        "Reset Password",
            //        $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            //    return RedirectToPage("./ForgotPasswordConfirmation");
            //}

            return Page();
        }
    }
}
