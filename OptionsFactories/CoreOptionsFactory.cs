using Microsoft.Extensions.Options;

namespace OptionsFactories
{
    /// <summary>
    /// An options factory that inherits from <see cref="IOptions{TOptions}"/> and actually works
    /// </summary>
    /// <typeparam name="TOptions">A class of properties that house options</typeparam>
    public class CoreOptionsFactory<TOptions> : IOptions<TOptions> where TOptions : class
    {
        #region Properties
        public TOptions Value { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an instance of <see cref="CoreOptionsFactory{TOptions}"/> using <see cref="TOptions"/>
        /// </summary>
        /// <param name="options">An instance of <see cref="TOptions"/></param>
        public CoreOptionsFactory(TOptions options)
        {
            Value = options;
        }
        #endregion
    }
}
