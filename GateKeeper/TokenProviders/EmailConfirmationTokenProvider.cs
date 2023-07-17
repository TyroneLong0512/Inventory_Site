using Microsoft.AspNetCore.Identity;

namespace GateKeeper.TokenProviders
{
    public class EmailConfirmationTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser> where TUser : class
    {
        #region Public Methods
        public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user) =>
            true;
        #endregion
    }
}
