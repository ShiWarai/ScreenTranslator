using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScreenTranslator_MainApp.ViewModel;

namespace ScreenTranslator_MainApp.Model
{
    class NotificatorClipboardText : Notificator
    {
        public NotificatorClipboardText()
        {
            DeactivatorEvent += NotificatorClipboardText_DeactivatorEvent;
        }

        private void NotificatorClipboardText_DeactivatorEvent()
        {
            var text = new ClipboardText();
            MainWindowVM.ThreadsControl.Start(ShowTextNotification, text);
        }

        void ShowTextNotification(object text)
        {
            ShowNotification(LanguageData.GetStringFromResource("aTranslation"), ((ClipboardText)text).TranslatedText, Notifications.Wpf.NotificationType.Information);
        }
    }
}
