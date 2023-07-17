using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GateKeeper.Contexts
{
    public class AuthenticationDBContext<TUser> : IdentityDbContext where TUser : IdentityUser
    {
        #region Fields
        private string _connectionString;
        #endregion

        #region Properties
        public DbSet<TUser>? FormsUser { get; set; }
        #endregion

        #region Constructors
        public AuthenticationDBContext(DbContextOptions options) : base(options)
        {
            _connectionString = string.Empty;
        }

        public AuthenticationDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion
    }
}
