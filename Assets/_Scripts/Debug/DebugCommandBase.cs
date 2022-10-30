namespace Project.Debug
{
    public class DebugCommandBase
    {
        public string CommandId { get; private set; }
        public string CommandDescription { get; private set; }
        public string CommandFormat { get; private set; }

        public DebugCommandBase(string id, string description, string format)
        {
            CommandId = id;
            CommandDescription = description;
            CommandFormat = format;
        }
    }
}