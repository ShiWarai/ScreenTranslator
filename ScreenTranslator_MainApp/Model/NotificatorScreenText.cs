using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenTranslator_MainApp.ViewModel;
using System.Windows.Forms;

namespace ScreenTranslator_MainApp.Model
{
    class NotificatorScreenText : Notificator
    {

        private MouseRectangle CursorRectangle;

        public NotificatorScreenText()
        {
            this.CursorRectangle = new MouseRectangle();
            ActivatorEvent += NotificatorScreenText_ActivatorEvent;
            DeactivatorEvent += NotificatorScreenText_DeactivatorEvent;
        }

        private void NotificatorScreenText_DeactivatorEvent()
        {
            if (this.CursorRectangle.Second.isEmpty)
            {
                CursorRectangle.Second = MouseCoordinator.GetCurrentMousePosition();
                CursorRectangle_CompleteInit();
            }
        }

        private void NotificatorScreenText_ActivatorEvent()
        {
            if (this.CursorRectangle.First.isEmpty)
                CursorRectangle.First = MouseCoordinator.GetCurrentMousePosition();
        }

        private void CursorRectangle_CompleteInit()
        {
            var screenText = new ScreenText(this.CursorRectangle);
            ShowNotification(LanguageData.GetStringFromResource("aTranslation") +":", screenText.TranslatedText, NotificationType.Information);
            this.CursorRectangle = new MouseRectangle();
        }
    }
}
