using GateKeeper.Interfaces;
using Librarian.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace XPros_Stock_And_Inventory.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        #region Fields
        private IUser<Guid> _user;
        private IDataOperator _ops;
        #endregion

        #region Properties
        [BindProperty]
        public UserInput Input { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; }
        #endregion

        public RegisterModel(IUser<Guid> user, IUserInfo<Guid> info, IDataOperator ops)
        {
            _user = user;
            Input = (UserInput)info;
            _ops = ops;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Users =_ops.GetData<IdentityUser>("SELECT * FROM AspNetUsers");
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Index");
            _user.SetInfo(Input);

            if (_user.Register(Input.Password).Succeeded)
                return RedirectToPage(returnUrl);
            else
                ModelState.AddModelError(string.Empty, "Failed to register the user");
            
            return Page();
        }
    }
}
