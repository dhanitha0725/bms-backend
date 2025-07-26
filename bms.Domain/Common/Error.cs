namespace bms.Domain.Common
{
    public class Error(string message)
    {
        public string Message { get; } = message;

        public override string ToString()
        {
            return Message;
        }
    }
}
