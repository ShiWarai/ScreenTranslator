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
using System.IO;

namespace ScreenTranslator_MainApp.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public MainWindow MainWin; // Основное окно
        static private LanguageData LanguageResource;
        static public Logger TranslationLogger;
        static public ThreadsMaster ThreadsControl = new ThreadsMaster();
        private KeyboardHooking KeyHook;
        

        public List<string> Languages
        {
            get
            {
                return SettingsManager.AllowedLanguages;
            }
        }
        public List<string> MethodsTranslate
        {
            get
            {
                return SettingsManager.TranslateMethods;
            }
        }

        public string KeyComb
        {
            get
            {
                return "None";
            }
        }

        public int SelectedMethodIndex
        {
            set 
            {
                if (value == -1)
                    return;
                if (value >= 0 && value < MethodsTranslate.Count)
                {
                    Properties.Settings.Default.TranslationWay = MethodsTranslate[value];
                    try
                    {
                        switch (MethodsTranslate[value])
                        {
                            case "Clipboard":
                                KeyboardHooking.ChangeFunction(new NotificatorClipboardText());
                                break;
                            case "Screen":
                                KeyboardHooking.ChangeFunction(new NotificatorScreenText());
                                break;
                        }
                    }
                    catch { }
                }
            }
        }

        public int SelectedLanguageIndex
        {
            set
            {
                if (value == -1)
                    return;
                if (value >= 0 && value < Languages.Count)
                    Properties.Settings.Default.Language = Languages[value];
                else
                    System.Windows.MessageBox.Show(LanguageResource.GetString("NotLanguage"), LanguageResource.GetString("Error"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool LoggerIsEnabled
        {
            set
            {
                try
                {
                    TranslationLogger.SetEnable(value);
                }
                catch { }
            }
        }

        public async Task ShowWindow() 
        {
            MainWin = new MainWindow();

            SetupLanguageDependences();
            WindowAdditionalInitialize();
            EventBinding();
            SetupSettings();
            LoggerInit();

            await MainWin.Dispatcher.InvokeAsync(() => MainWin.ShowDialog());

            ThreadsControl.KillThreads();
            SaveSettings(); // Сохраняем настройки перед выходом
        }


        private void SetupSettings()
        {
            int selected;

            selected = Languages.IndexOf(Properties.Settings.Default.Language);
            MainWin.LanguageSelect.ItemsSource = Languages;
            MainWin.LanguageSelect.SelectedIndex = selected;

            selected = MethodsTranslate.IndexOf(Properties.Settings.Default.TranslationWay);
            MainWin.TranslateWaySelect.ItemsSource = MethodsTranslate;
            MainWin.TranslateWaySelect.SelectedIndex = selected;

            MainWin.KeyCombSelect.Text = Properties.Settings.Default.KeyComb;

            if (Properties.Settings.Default.LoggerIsEnabled)
                MainWin.LoggerIsEnabled.IsChecked = true;

            if (File.Exists(Properties.Settings.Default.LogWay))
                MainWin.LogWayView.Text = Properties.Settings.Default.LogWay;
            else
                MainWin.LogWayView.Text = "";

        }

        private void LoggerInit()
        {
            if (File.Exists(MainWin.LogWayView.Text))
            {
                TranslationLogger = new Logger(MainWin.LogWayView.Text);
                TranslationLogger.WriteToLog("!!! Start logging !!!");
            }
            else
            {
                TranslationLogger = new Logger("");
            }
            TranslationLogger.SetEnable(Properties.Settings.Default.LoggerIsEnabled);
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.KeyComb = MainWin.KeyCombSelect.Text;
            Properties.Settings.Default.Language = MainWin.LanguageSelect.SelectedItem.ToString();
            Properties.Settings.Default.TranslationWay = MainWin.TranslateWaySelect.SelectedItem.ToString();
            Properties.Settings.Default.LogWay = MainWin.LogWayView.Text;
            TranslationLogger.WriteToLog("!!! End logging !!!");
            Properties.Settings.Default.LoggerIsEnabled = MainWin.LoggerIsEnabled.IsChecked.Value;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Additional UI initialize of WinForms objects
        /// </summary>
        private void WindowAdditionalInitialize()
        {
            MainWin.Notify = new NotifyIcon();
            using (Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/ScreenTranslator_MainApp;component/Properties/icon_w.ico")).Stream)
            {
                MainWin.Notify.Icon = new System.Drawing.Icon(iconStream);
            }
            MainWin.Notify.Visible = false;
        }

        private void EventBinding()
        {
            MainWin.StateChanged += MainWindowState;
            MainWin.KeyCombSelect.GotFocus += KeyCombSelect_GotFocus;
            MainWin.KeyCombSelect.LostFocus += KeyCombSelect_LostFocus;
            MainWin.ChangeLogWay.Click += ChangeLogWay_Click;

            switch (Properties.Settings.Default.TranslationWay)
            {
                case "Clipboard":
                    KeyHook = new KeyboardHooking(new NotificatorClipboardText());
                    break;
                case "Screen":
                    KeyHook = new KeyboardHooking(new NotificatorScreenText());
                    break;
            }
            KeyHook.Start(SettingsManager.KeyFromString(Properties.Settings.Default.KeyComb));
        }

        private void ChangeLogWay_Click(object sender, RoutedEventArgs e)
        {
            string way = Logger.CreateLogFile();
            TranslationLogger = new Logger(way);
            TranslationLogger.SetEnable(true);
            MainWin.LogWayView.Text = way;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void MainWindowState(object sender, EventArgs e)
        {
            if (MainWin.WindowState == WindowState.Minimized)
                Minimized();
            else if (MainWin.WindowState == WindowState.Normal)
                Maximized();
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

        private void KeyCombSelect_GotFocus(object sender, RoutedEventArgs e)
        {
            MainWin.KeyCombSelect.FontWeight = FontWeights.Bold;
            MainWin.PreviewKeyDown += MainWin_KeyUp;
            KeyHook.Pause();
        }

        private void MainWin_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            MainWin.KeyCombSelect.Text = e.Key.ToString();
        }

        private void KeyCombSelect_LostFocus(object sender, RoutedEventArgs e)
        {
            MainWin.KeyCombSelect.FontWeight = FontWeights.Normal;
            MainWin.KeyUp -= MainWin_KeyUp;
            KeyHook.Start(SettingsManager.KeyFromString(MainWin.KeyCombSelect.Text));
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
                MainWin.LogWayTitle.Content = LanguageResource.GetString("LogWayTitle");
                MainWin.LoggerIsEnabled.Content = LanguageResource.GetString("LoggerIsEnabled");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
