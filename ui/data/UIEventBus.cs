using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iterate.ui.data
{
    public class UIEventBus : INotifier
    {
        public UIDataHandler<Notification> Notification = new UIDataHandler<Notification>("notification", new ui.Notification {loading = false, message = "Event bus suucessfully connected" });

        public void Notify(Notification notification)
        {
            Notification.UpdateNotification(notification);
        }
    }
}
