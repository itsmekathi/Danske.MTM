namespace Danske.MTM.Common.Exceptions
{
    public class InvalidRequestException : BaseApplicationException
    {
        public InvalidRequestException(string message)
            : base(message)
        {
        }
    }
}
