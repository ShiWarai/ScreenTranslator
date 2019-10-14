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
using System.Reflection;
using System.IO;

namespace ScreenTranslator_MainApp.ViewModel
{
    public class AppManager : INotifyPropertyChanged
    {
        public MainWindow MainWin;
        private ResourceManager LanguageResource;

        public ResourceManager GetLanguageResource { get { return LanguageResource; } }

        public async Task ShowWindow() 
        {
            MainWin = new MainWindow();
            SetupLanguageDependences();
            MainWin.DoneButton.Click += DoneButton_Click;
            MainWin.StateChanged += MainWindowState;
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

        private string ClipboardTranslate()
        {
            string text = Clipboard.GetText();
            return (new Translator("trnsl.1.1.20191013T000841Z.a1691e726d3b0db8.19a9f0b65a94f0e6f7aea4f246947a0b3fe1ee84")).Translate(text, Properties.Settings.Default.Language);
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
            ShowNotification(LanguageResource.GetString("Title"), "Window has been minimized, but still working!", NotificationType.Warning);
            try
            {
                MainWin.Notify.Visible = true;
                MainWin.Notify.Click += Notify_Click;
                MainWin.WindowState = WindowState.Minimized;
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
            }
            catch { }
        }

        private bool SetupLanguageDependences() // Настройка переменных, зависящих от языка приложения
        {
            if (LanguageResource == null)
                this.LanguageResource = new ResourceManager("ScreenTranslator_MainApp.Lang.lang", Assembly.GetExecutingAssembly());
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

        private void ShowNotification(string title, string message, NotificationType type)
        {
            var notification = new NotificationManager();
            notification.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type
            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
