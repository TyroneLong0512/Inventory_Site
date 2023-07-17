using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GateKeeper.UserManagers
{
    public class FormsUserManager<TUser> : UserManager<TUser>, IUserManager<TUser, Guid> where TUser : IdentityUser, IUser<Guid>
    {
        #region Constructor
        /// <summary>
        /// Constructs a new instance of the IdentityUserManager implementation for the IUserManager interface
        /// </summary>
        /// <param name="store">The User store to use for storing user information</param>
        /// <param name="optionAccessor">The options relating to the Identity Authentication</param>
        /// <param name="passwordHasher">An empty implementation of the Password hasher used by Identity</param>
        /// <param name="userValidators">An empty implementation of the User validator used by Identity</param>
        /// <param name="passwordValidators">An empty implementation of the Password validator used by Identity</param>
        /// <param name="lookupNormalizer">An UpperInvariantLookupNormalizer for normalizing key names in user classes</param>
        /// <param name="errors">A descriptor for errors encountered by the authentication service</param>
        /// <param name="services">No clue</param>
        /// <param name="logger">A logger that will log all the events that transpire during authentication</param>
        public FormsUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer lookupNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger)
            : base(store, optionAccessor, passwordHasher, userValidators, passwordValidators, lookupNormalizer, errors, services, logger)
        {
            
        }
        #endregion

        #region Public Methods
        public IdentityResult CreateUser(TUser user, string password)
        {
            IdentityResult result = CreateAsync(user, password).Result;
            if (result == IdentityResult.Success)
            {
                IUser<Guid> createdUser = FindByIdAsync(user.GetUserId().ToString()).Result;
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.ToArray());
        }

        public IdentityResult CreateUser(TUser user) =>
            CreateAsync(user).Result;

        public bool DeleteUser(TUser user) =>
            DeleteAsync(user).Result.Succeeded;

        public bool UpdateUser(TUser user) =>
            UpdateUserAsync(user).Result.Succeeded;

        public bool CreateDataBaseSchema()
        {
            //ToDo: Tyrone - Work out how to create the schema by default when not present
            //UserStore<FormsUser> userStore = (UserStore<FormsUser>)Store;
            //bool returnValue = userStore.Context.Database.EnsureCreated();
            //string generationScript = userStore.Context.Database.GenerateCreateScript();

            return false;
        }

        public IdentityResult ConfirmUser(TUser user, string token, string password)
        {
            IdentityResult result = ConfirmEmailAsync(user, token).Result;
            if (result == IdentityResult.Success)
            {
                AddPasswordAsync(user, password);
                return IdentityResult.Success;
            }
                
            return IdentityResult.Failed(result.Errors.ToArray());
        }
        #endregion

        #region Override Methods
        protected override async Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<TUser> store, TUser user, string password)
        {
            var hash = await store.GetPasswordHashAsync(user, CancellationToken);
            if (hash == null)
            {
                return PasswordVerificationResult.Failed;
            }
            return PasswordHasher.VerifyHashedPassword(user, hash, password);
        }
        #endregion
    }
}
