using GateKeeper.Attributes;

namespace GateKeeper.Interfaces
{
    public interface IUserInfo<TKey>
    {
        TKey Id { get; set; }
        [SearchKey]
        string UserName { get; set; }
        [SearchKey]
        string NormalizedUserName { get; set; }
        [SearchKey]
        string Email { get; set; }
        [SearchKey]
        string NormalizedEmail { get; set; }
        string Password { get; set; }
        IContext Context { get; set; }
    }
}
