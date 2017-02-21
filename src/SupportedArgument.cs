namespace CongressCollector
{
    public class SupportedArgument<T>
    {
        public T Value { get; private set; }
        public string Description { get; private set; }

        public SupportedArgument(T value, string description)
        {
            Value = value;
            Description = description;
        }
    }
}