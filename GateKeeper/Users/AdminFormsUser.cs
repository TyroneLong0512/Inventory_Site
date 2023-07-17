using GateKeeper.Interfaces;
using GateKeeper.Users.BaseClasses;
using Microsoft.AspNetCore.Identity;

namespace GateKeeper.Users
{
    /// <summary>
    /// Derived class from FormsUser base class for Admin logins
    /// </summary>
    public class AdminFormsUser : FormsUser<AdminFormsUser>
    {
        #region Constructors
        /// <summary>
        /// Constructs an AdminFormsUser using the userInfo supplied along with the base FormsUser class
        /// </summary>
        /// <param name="userInfo">An instance of a class that implements IUserInfo and was bound to the front end</param>
        public AdminFormsUser(IUserInfo<Guid> userInfo) : base(userInfo)
        {

        }

        /// <summary>
        /// Not to be used by the developer. Identity requires a parameterless constructor
        /// </summary>
        public AdminFormsUser() : base()
        {

        }
        #endregion

        #region Public Methods
        public override string GetUserName() =>
            _userManager.GetUserNameAsync(this).Result;

        public override Guid GetUserId() =>
            new Guid(_userManager.GetUserIdAsync(this).Result);

        public override IdentityResult Register(string password) =>
            _userManager.CreateUser(this, password);

        public override IdentityResult ConfirmUser(string token, string password) =>
            IdentityResult.Success;
        #endregion
    }
}
