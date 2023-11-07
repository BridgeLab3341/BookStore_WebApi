using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CommonLayer.Models
{
    public class MsmqModel
    {
        MessageQueue bookQueue = new MessageQueue();
        public void SendData2Queue(string token)
        {
            bookQueue.Path = @".\private$\Token";
            if (!MessageQueue.Exists(bookQueue.Path))
            {
                MessageQueue.Create(bookQueue.Path);
            }
            bookQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            bookQueue.ReceiveCompleted += BookStore_ReceiveCompleted; //Subscribes method to the Receive event of the bookQueue. This event is triggered when a message is received.
            bookQueue.Send(token);
            bookQueue.BeginReceive();
            bookQueue.Close();
        }
        public void BookStore_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = bookQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string body = $"<a style = \"color:#00802b; text-decoration: none; font-size:20px;\" href='http://localhost:4200/resetpassword/{token}'>Click me</a>\n";
                string subject = "Token for reset password";
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("somubridgelabz@gmail.com", "crauayaudvfyhumw"),
                    EnableSsl = true,
                };
                smtp.Send("somubridgelabz@gmail.com", "somubridgelabz@gmail.com", subject, body);
                bookQueue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                throw ex;
            }
        }


    }
}
