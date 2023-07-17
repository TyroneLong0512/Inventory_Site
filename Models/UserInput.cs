using Models.Interfaces;
using GateKeeper.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// Model to be used for user inputs
    /// </summary>
    public class UserInput : IUserInfo<Guid>, IModel<Guid>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IContext Context { get; set; }

        /// <summary>
        /// Constructs an instance of <see cref="UserInput"/> with an instance of <see cref="IContext"/>, usually though dependency injection
        /// </summary>
        /// <param name="context">An implementation of <see cref="IContext"/>, usually supplied through dependency injection</param>
        public UserInput(IContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Constructs an instance of <see cref="UserInput"/> without any parameters
        /// </summary>
        public UserInput()
        {

        }
    }
}
