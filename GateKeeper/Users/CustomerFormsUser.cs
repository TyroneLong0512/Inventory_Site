using GateKeeper.Interfaces;
using GateKeeper.Users.BaseClasses;
using Microsoft.AspNetCore.Identity;

namespace GateKeeper.Users
{
    public class CustomerFormsUser : FormsUser<CustomerFormsUser>
    {
        #region Constructors
        public CustomerFormsUser(IUserInfo<Guid> userInfo) : base(userInfo)
        {
            
        }

        public CustomerFormsUser() : base() { }
        #endregion        

        #region Public Methods
        public override string GetUserName() =>
            _userManager.GetUserNameAsync(this).Result;

        public override Guid GetUserId() =>
            new Guid(_userManager.GetUserIdAsync(this).Result);

        public override IdentityResult Register(string password = null)
        {
            try
            {
                IdentityResult result = _userManager.CreateUser(this);
                if (result == IdentityResult.Success)
                {
                    string token = _userManager.GenerateEmailConfirmationTokenAsync(this).Result;
                        return IdentityResult.Success;
                }

                return IdentityResult.Failed(result.Errors.ToArray());
            }
            catch (Exception)
            {
                _userManager.DeleteUser(this);
                return IdentityResult.Failed(new IdentityError() { Code = "0001", Description = "Something went wrong! Sign up attempt reset" });
            }
        }

        public override IdentityResult ConfirmUser(string token, string password) =>
            _userManager.ConfirmUser(GetUser(), token, password);
        #endregion
    }
}
