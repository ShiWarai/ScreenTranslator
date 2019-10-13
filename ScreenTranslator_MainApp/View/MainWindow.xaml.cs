using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScreenTranslator_MainApp.ViewModel;
using Notifications.Wpf;

namespace ScreenTranslator_MainApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceManager LanguageResource;
        private NotificationWin MainNotification;

        public ResourceManager GetLanguageResource
        {
            get { return this.LanguageResource; }
        }


        public MainWindow()
        {
            InitializeComponent();
            SetupLanguageDependences();
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private bool SetupLanguageDependences() // Настройка переменных, зависящих от языка приложения
        {
            if (LanguageResource==null)
                this.LanguageResource = new ResourceManager("ScreenTranslator_MainApp.Lang.lang", Assembly.GetExecutingAssembly());
            try
            {
                Title = LanguageResource.GetString("Title");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Main_Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
                Minimized();
            else if (this.WindowState == WindowState.Normal)
                Maximized();
        }
        private void Minimized()
        {
            this.Hide();
            ShowNotification(LanguageResource.GetString("Title"), "Window has been minimized, but still working!", NotificationType.Warning);
            try
            {
                this.MainNotification = new NotificationWin(this);
                this.MainNotification.Show();
                this.MainNotification.Hide();
            }
            catch{}
        }

        private void Maximized()
        {
            try
            {
                this.MainNotification.Close();
            }
            catch
            {

            }
        }

        private void ShowNotification(string title,string message,NotificationType type)
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
