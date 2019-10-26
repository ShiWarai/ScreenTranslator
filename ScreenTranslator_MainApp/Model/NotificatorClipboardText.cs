using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTranslator_MainApp.Model
{
    class NotificatorClipboardText : Notificator
    {
        public NotificatorClipboardText()
        {
            ActivatorEvent += NotificatorClipboardText_ActivatorEvent;
        }

        private void NotificatorClipboardText_ActivatorEvent()
        {
            var text = new ClipboardText();
            ShowNotification(LanguageData.GetStringFromResource("aTranslation"), text.TranslatedText, Notifications.Wpf.NotificationType.Information);
        }
    }
}
