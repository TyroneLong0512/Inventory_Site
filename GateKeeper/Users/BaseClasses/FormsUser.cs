using GateKeeper.Attributes;
using GateKeeper.Authenticators;
using GateKeeper.ClaimsPrincipalFactories;
using GateKeeper.Contexts;
using GateKeeper.Hashers;
using GateKeeper.Interfaces;
using GateKeeper.TokenProviders;
using GateKeeper.UserManagers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsFactories;
using System.Reflection;

namespace GateKeeper.Users.BaseClasses
{
    /// <summary>
    /// The Identity User implementation for the IUser interface
    /// </summary>
    public abstract class FormsUser<TUser> : IdentityUser, IUser<Guid> where TUser : IdentityUser, IUser<Guid>, new()
    {
        #region Fields
        protected FormsUserManager<TUser>? _userManager;
        protected FormsAuthenticator<TUser>? _authenticator;
        private DbContext _context;
        private IContext _userContext;
        protected int? _systemEntityId;
        #endregion

        #region Constructors
        /// <summary>
        /// Contructs a FormsUser with a user name that uses the IdentityUserManager to facilitate actions on the user. Also provides the context for the authentication
        /// </summary>
        /// <param name="userInfo">An instance of the IUserInfo interface</param>
        public FormsUser(IUserInfo<Guid> userInfo) : base()
        {
            if (userInfo.Context != null)
            {
                _userContext = userInfo.Context;
                InitializeDbContext(userInfo.Context.ConnectionString);
                Dictionary<string, TokenProviderDescriptor> tokenDescriptor = new Dictionary<string, TokenProviderDescriptor>();
                tokenDescriptor.Add("BaseTokenProvider", new TokenProviderDescriptor(typeof(EmailConfirmationTokenProvider<TUser>)) { ProviderInstance = new EmailConfirmationTokenProvider<TUser>() });

                IOptions<IdentityOptions> identityOptions = CreateOptions(new IdentityOptions()
                {
                    Tokens = new TokenOptions() { EmailConfirmationTokenProvider = "SBS_TokenProvider", ProviderMap = tokenDescriptor }
                });
                ServiceCollection collection = new ServiceCollection();
                collection.AddScoped<IUserTwoFactorTokenProvider<TUser>, EmailConfirmationTokenProvider<TUser>>();

                _userManager = new FormsUserManager<TUser>(new UserStore<TUser>(_context), identityOptions,
                    new HMAC_SHA256Hasher<TUser>(), CreateUserValidators(), CreatePasswordValidators(), new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), collection.BuildServiceProvider(),
                    new Logger<FormsUserManager<TUser>>(new LoggerFactory()));
                _authenticator = new FormsAuthenticator<TUser>(_userManager, new HttpContextAccessor() { HttpContext = userInfo.Context.Context }, new DefaultClaimsPrincipalFactory<TUser>(_userManager, identityOptions), identityOptions,
                    new Logger<FormsAuthenticator<TUser>>(new LoggerFactory()),
                    new AuthenticationSchemeProvider(CreateOptions(new AuthenticationOptions() { DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme })),
                    new DefaultUserConfirmation<TUser>());
            }
        }

        /// <summary>
        /// This constructor is for internal use only.
        /// </summary>
        public FormsUser() : base()
        {

        }
        #endregion

        #region Private Methods
        private void InitializeUser(IUserInfo<Guid> userInfo)
        {
            foreach (PropertyInfo prop in typeof(IUserInfo<Guid>).GetProperties())
                if (GetType().GetProperty(prop.Name) != null && prop.PropertyType == GetType().GetProperty(prop.Name).PropertyType)
                    if (prop.GetValue(userInfo) != null)
                        GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(userInfo));
        }

        private IOptions<TOptions> CreateOptions<TOptions>(TOptions options) where TOptions : class =>
            new CoreOptionsFactory<TOptions>(options);

        private IEnumerable<IUserValidator<TUser>> CreateUserValidators() =>
            new List<IUserValidator<TUser>>();

        private IEnumerable<IPasswordValidator<TUser>> CreatePasswordValidators() =>
            new List<IPasswordValidator<TUser>>();

        private void InitializeDbContext(string connectionString) =>
            _context = new AuthenticationDBContext<TUser>(new DbContextOptionsBuilder().UseSqlServer(new SqlConnection(connectionString)).Options);
        #endregion

        #region Protected Methods
        protected TUser? GetUser()
        {
            foreach (PropertyInfo info in typeof(IUserInfo<Guid>).GetProperties().Where(prop => prop.GetCustomAttribute(typeof(SearchKeyAttribute)) != null))
                if (GetType().GetProperty(info.Name).GetValue(this) != null)
                    switch (info.Name)
                    {
                        case "UserName":
                            return _userManager.Users.Where(usr => usr.UserName == UserName).FirstOrDefault();
                        case "NormalizedUserName":
                            return _userManager.Users.Where(usr => usr.NormalizedUserName == NormalizedUserName).FirstOrDefault();
                        case "Email":
                            return _userManager.Users.Where(usr => usr.Email == Email).FirstOrDefault();
                        case "NormalizedEmail":
                            return _userManager.Users.Where(usr => usr.NormalizedEmail == NormalizedEmail).FirstOrDefault();
                        default:
                            break;
                    }

            return null;
        }
        #endregion

        #region Public Methods
        public void SetInfo(IUserInfo<Guid> userInfo) =>
            InitializeUser(userInfo);

        public SignInResult SignIn(string password)
        {
            TUser? user = GetUser();
            return user != null ? _authenticator.PasswordSignIn(user, password) : SignInResult.NotAllowed;
        }

        public bool SignOut()
        {
            try
            {
                _authenticator.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public IContext GetUserContext() =>
            _userContext;
        #endregion

        #region Abstract Methods
        public abstract IdentityResult Register(string password = null);

        public abstract Guid GetUserId();

        public abstract string GetUserName();

        public abstract IdentityResult ConfirmUser(string token, string password);
        #endregion
    }
}
