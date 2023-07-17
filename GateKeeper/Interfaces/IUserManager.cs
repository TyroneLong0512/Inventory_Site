using Microsoft.AspNetCore.Identity;

namespace GateKeeper.Interfaces
{
    /// <summary>
    /// Interface to be implemented by user management classes
    /// </summary>
    public interface IUserManager<TUser, TUserKey> where TUser : IUser<TUserKey>
    {
        /// <summary>
        /// Creates a user of type IUser
        /// </summary>
        /// <param name="user">The implementation of IUser to create</param>
        /// <param name="password">The password assigned to the user (unhashed)</param>
        /// <returns>IdentityResult</returns>
        IdentityResult CreateUser(TUser user, string password);

        /// <summary>
        /// Creates a user without create a system entity or assigning a password to the user.
        /// </summary>
        /// <param name="user">The implementation of IUser to create</param>
        /// <returns>IdentityResult</returns>
        IdentityResult CreateUser(TUser user);

        /// <summary>
        /// Updates the user with the new information specified in IUser
        /// </summary>
        /// <param name="user">The implementation of IUser to use for the update</param>
        /// <returns>bool</returns>
        bool UpdateUser(TUser user);

        /// <summary>
        /// Deletes the a user of type IUser
        /// </summary>
        /// <param name="user">The implementation of IUser to use for the delete</param>
        /// <returns>bool</returns>
        bool DeleteUser(TUser user);

        /// <summary>
        /// Creates the tables relating to the context used in the UserStore
        /// </summary>
        /// <returns>bool</returns>
        bool CreateDataBaseSchema();

        /// <summary>
        /// Confirms a user's email account and creates the relevant system entity
        /// </summary>
        /// <param name="user">The implementation of IUser to use for confirmation</param>
        /// <param name="token">The token to use for the confirmation</param>
        /// <param name="password">The password the user is to be confirmed with</param>
        /// <param name="systemEntityId">The system entity Id assigned to the user after confirmation</param>
        /// <returns>IdentityResult</returns>
        IdentityResult ConfirmUser(TUser user, string token, string password);
    }
}
