using Framework.Core.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common
{
    //public class MessageNotification : IMessageNotification
    //{

    //    public void SendHtmlMail( string destination, string destinationDisplayName, string subject, string body)
    //    {
    //        try
    //        {
    //            MailMessage _ms = new MailMessage();
    //            SmtpClient _smtp = new SmtpClient("mail.sabinarya.com", 25);

    //            _smtp.EnableSsl = false;
    //            _smtp.Credentials = new NetworkCredential("noreply@sabinarya.com", "123456aA@", "");

    //            _ms.From = new MailAddress("noreply@sabinarya.com", "noreply");
    //            _ms.To.Add(new MailAddress(destination, destinationDisplayName));

    //            _ms.Subject = subject;
    //            _ms.IsBodyHtml = true;
    //            _ms.Body = body;
    //            _smtp.Send(_ms);
    //        }
    //        catch (Exception ex)
    //        {
    //            var exception = ex;
    //        }
    //    }
    //}
}
