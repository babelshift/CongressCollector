namespace CongressCollector
{
    /// <summary>
    /// Supported arguments are values that can be passed in to this application for the purposes of user input interaction
    /// </summary>
    public class SupportedArgument<T>
    {
        /// <summary>
        /// Value of the supported argument which can be passed in by a user to the application
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Descripton of the supported argument which is displayed in help outputs
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Default constructor requires the value and descripton for the supported argument
        /// </summary>
        /// <param name="value">Value of the supported argument which can be passed in by a user to the application</param>
        /// <param name="description">Descripton of the supported argument which is displayed in help outputs</param>
        public SupportedArgument(T value, string description)
        {
            Value = value;
            Description = description;
        }
    }
}