using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace GateKeeper.ClaimsPrincipalFactories
{
    /// <summary>
    /// Class to allow for additional claim creation on the UserPrincipal created on the context
    /// </summary>
    /// <typeparam name="TUser">The type of the user to create the claims for</typeparam>
    /// <typeparam name="TUserKey">The type of the key for user class</typeparam>
    public class DefaultClaimsPrincipalFactory<TUser> : UserClaimsPrincipalFactory<TUser> where TUser : IdentityUser, IUser<Guid>
    {
        private UserManager<TUser> _userManager;

        /// <summary>
        /// Default contructor for the factory
        /// </summary>
        /// <param name="userManager">The user manager that the factory will function off of</param>
        /// <param name="optionsAccessor">The Identity options relating to the claims factory</param>
        public DefaultClaimsPrincipalFactory(UserManager<TUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            string userId = await UserManager.GetUserIdAsync(user);

            ClaimsIdentity id = new("Identity.Application",
                Options.ClaimsIdentity.UserNameClaimType,
                Options.ClaimsIdentity.RoleClaimType);

            id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, user.Email));

            if (UserManager.SupportsUserEmail)
            {
                string email = await UserManager.GetEmailAsync(user);
                if (!string.IsNullOrEmpty(email))
                    id.AddClaim(new Claim(Options.ClaimsIdentity.EmailClaimType, email));
            }

            if (UserManager.SupportsUserSecurityStamp)
                id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                    await UserManager.GetSecurityStampAsync(user)));

            if (UserManager.SupportsUserClaim)
                id.AddClaims(await UserManager.GetClaimsAsync(user));

            return id;
        }
    }
}
