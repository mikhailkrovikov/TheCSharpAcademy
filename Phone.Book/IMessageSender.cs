namespace Phone.Book
{
    public interface IMessageSender
    {
        Task Send(string to, string address, string subject, string body);
    }
}