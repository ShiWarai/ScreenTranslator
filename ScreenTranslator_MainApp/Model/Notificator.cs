using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTranslator_MainApp.Model
{
    abstract class Notificator : WaitingForKeyEvent
    {
        static public void ShowNotification(string title, string message, NotificationType type)
        {
            var notification = new NotificationManager();
            notification.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type
            });
        }
    }
}
