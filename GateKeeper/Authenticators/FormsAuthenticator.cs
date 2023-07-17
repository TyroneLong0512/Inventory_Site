using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GateKeeper.Authenticators
{

    public class FormsAuthenticator<TUser> : SignInManager<TUser>, IAuthenticate<TUser, Guid> where TUser : IdentityUser, IUser<Guid>
    {
        #region Constructors
        public FormsAuthenticator(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<FormsAuthenticator<TUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<TUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            
        }
        #endregion

        #region Public Methods
        public bool IsSignedIn(TUser user) =>
            IsSignedIn(CreateUserPrincipalAsync(user).Result);

        public SignInResult PasswordSignIn(string userName, string password) =>
            PasswordSignInAsync(userName, password, true, false).Result;

        public SignInResult PasswordSignIn(TUser user, string password) =>
            PasswordSignInAsync(user, password, true, false).Result;
        #endregion
    }
}