using System;
using System.Collections.Generic;
using System.Text;

namespace Phone.Book
{
    public interface IMessageSender
    {
        void Send(string to, string subject, string body);
    }
    public class MailSender : IMessageSender
    {
        public void Send(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}


