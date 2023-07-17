using Microsoft.AspNetCore.Http;

namespace GateKeeper.Interfaces
{
    public interface IContext
    {
        string ConnectionString { get; set; }

        HttpContext Context { get; set; }
    }
}