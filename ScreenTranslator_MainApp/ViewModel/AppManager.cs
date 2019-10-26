using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.ComponentModel;
using ScreenTranslator_MainApp.Model;
using ScreenTranslator_MainApp.View;
using Notifications.Wpf;
using System.Resources;
using System.Windows.Forms;

namespace ScreenTranslator_MainApp.ViewModel
{
    public class AppManager : INotifyPropertyChanged
    {
        public MainWindow MainWin; // Основное окно
        private LanguageData LanguageResource;
        private KeyboardHooking KeyHook = new KeyboardHooking();

        public async Task ShowWindow() 
        {
            MainWin = new MainWindow();
            SetupLanguageDependences();
            MainWin.DoneButton.Click += DoneButton_Click;
            MainWin.StateChanged += MainWindowState;
            KeyHook.Start(Keys.G, new NotificatorScreenText());
            await MainWin.Dispatcher.InvokeAsync(() => MainWin.ShowDialog());
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            MainWin.WindowState=WindowState.Minimized;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void MainWindowState(object sender, EventArgs e)
        {
            if (MainWin.WindowState == WindowState.Minimized)
            {
                Minimized();
            }
            else if (MainWin.WindowState == WindowState.Normal)
            {
                Maximized();
            }
        }

        public void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimized()
        {
            Notificator.ShowNotification(LanguageResource.GetString("Title"), "Window has been minimized, but still working!", NotificationType.Warning);
            try
            {
                MainWin.Notify.Visible = true;
                MainWin.Notify.Click += Notify_Click;
                MainWin.WindowState = WindowState.Minimized;
                MainWin.ShowInTaskbar = false;
            }
            catch { }
        }

        private void Notify_Click(object sender, EventArgs e)
        {
            this.MainWin.Show();
            this.MainWin.WindowState = System.Windows.WindowState.Normal;
            this.MainWin.Activate();
        }

        private void Maximized()
        {
            try
            {
                MainWin.Notify.Visible = false;
                MainWin.ShowInTaskbar = true;
            }
            catch { }
        }

        private bool SetupLanguageDependences() // Настройка переменных, зависящих от языка приложения
        {
            LanguageResource = new LanguageData();
            try
            {
                MainWin.Title = LanguageResource.GetString("Title");
                MainWin.LanguageTitle.Content = LanguageResource.GetString("LanguageTitle");
                MainWin.TranslateWayTitle.Content = LanguageResource.GetString("TranslateWayTitle");
                MainWin.KeyCombTitle.Content = LanguageResource.GetString("ActivationButtonsTitle");
                MainWin.DoneButton.Content = LanguageResource.GetString("DoneTitle");
                MainWin.LogWayTitle.Content = LanguageResource.GetString("LogWayTitle");
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
