using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Notifications.Wpf;
using ScreenTranslator_MainApp.ViewModel;

namespace ScreenTranslator_MainApp.View
{
    public partial class NotificationWin : Form
    {
        private MainWindow Last_Window;
        public NotificationWin(MainWindow window)
        {
            InitializeComponent();
            Last_Window = window;
            SetupLanguageDependences();
        }

        private bool SetupLanguageDependences() // Настройка переменных, зависящих от языка приложения
        {
            try
            {
                NotifyIcon.Text = Last_Window.GetLanguageResource.GetString("Title");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Last_Window.Show();
            this.Last_Window.WindowState = System.Windows.WindowState.Normal;
            this.Last_Window.Text_Box.Text = (new MainWindowVM()).ClipboardTranslate();
        }
    }
}
