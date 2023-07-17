using Microsoft.AspNetCore.Identity;

namespace GateKeeper.Interfaces
{
    /// <summary>
    /// Interface to be implemented by Authentication classes
    /// </summary>
    public interface IAuthenticate<TUser, TUserKey> where TUser : IUser<TUserKey>
    {
        /// <summary>
        /// Sign in the user using a user name and password
        /// </summary>
        /// <param name="Username">The username for the given user</param>
        /// <param name="Password">The password for the given user</param>
        /// <returns>Task of SignInResult</returns>
        SignInResult PasswordSignIn(string userName, string password);

        /// <summary>
        /// Sign in the user using an IUser implementation
        /// </summary>
        /// <param name="user">The IUser implemntation to use for signing in</param>
        /// <param name="password">The password received as input from a source</param>
        /// <returns>Task of SignInResult</returns>
        SignInResult PasswordSignIn(TUser user, string password);

        /// <summary>
        /// Validate whether the user is signed in
        /// </summary>
        /// <returns>bool</returns>
        bool IsSignedIn(TUser user);
    }
}
