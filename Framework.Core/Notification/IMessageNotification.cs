using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Notification
{
    public interface IMessageNotification
    {
        void SendHtmlMail(string destination, string destinationDisplayName, string subject, string body);
    }
}
