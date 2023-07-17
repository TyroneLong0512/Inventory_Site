using GateKeeper.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GateKeeper.Users.BaseClasses
{
    public class UserContext : IContext
    {
        public UserContext(IHttpContextAccessor context, IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
            Context = context.HttpContext;
        }

        public UserContext(IHttpContextAccessor context)
        {
            Context = context.HttpContext;
        }
        
        public string ConnectionString { get; set; }

        public HttpContext Context { get; set; }
    }
}