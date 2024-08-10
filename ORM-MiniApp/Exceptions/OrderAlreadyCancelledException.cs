namespace ORM_MiniApp.Exceptions
{
    internal sealed class OrderAlreadyCancelledException : Exception
    {
        public OrderAlreadyCancelledException(string message) : base(message)
        {

        }
    }
}
