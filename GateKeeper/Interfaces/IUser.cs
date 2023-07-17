using Microsoft.AspNetCore.Identity;

namespace GateKeeper.Interfaces
{
    /// <summary>
    /// Interface to be implemented by User classes
    /// </summary>
    public interface IUser<TKey>
    {
        /// <summary>
        /// Retrieves the User's Id
        /// </summary>
        /// <returns>TKey</returns>
        TKey GetUserId();

        /// <summary>
        /// Retrieves the User's registered username
        /// </summary>
        /// <returns>string</returns>
        string GetUserName();

        /// <summary>
        /// Signs the user in using the IAuthenticate implementation specified in the user implementation
        /// </summary>
        /// <returns>SignInResult</returns>
        SignInResult SignIn(string password);

        /// <summary>
        /// Signs the user out, clearing all sign in related cookies.
        /// </summary>
        /// <returns>boolean</returns>
        bool SignOut();

        /// <summary>
        /// Registers a user of the given type in the data store
        /// </summary>
        /// <param name="Password">The password assigned to the user for registration</param>
        /// <returns>IdentityResult</returns>
        IdentityResult Register(string password = null);

        /// <summary>
        /// Sets the info for the user. Required before sign in is performed
        /// </summary>
        /// <param name="userInfo">An instance of a class that implements the IUserInfo interface</param>
        void SetInfo(IUserInfo<TKey> userInfo);

        /// <summary>
        /// Retrieves the context set for the user
        /// </summary>
        /// <returns>IContext</returns>
        IContext GetUserContext();

        /// <summary>
        /// Confirms the user's email account
        /// </summary>
        /// <param name="token">The token entered by the user</param>
        /// <param name="password">The password the user is to be registered with</param>
        /// <returns>IdentityResult</returns>
        IdentityResult ConfirmUser(string token, string password);
    }
}
