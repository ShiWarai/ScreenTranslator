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
using System.Windows.Forms;
using System.IO;

namespace ScreenTranslator_MainApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NotifyIcon Notify;
        public MainWindow()
        {
            InitializeComponent();
            AdditionalInitialize();
        }

        /// <summary>
        /// Additional UI initialize of WinForms objects
        /// </summary>
        private void AdditionalInitialize()
        {
            this.Notify = new NotifyIcon();
            using (Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/ScreenTranslator_MainApp;component/Properties/icon_w.ico")).Stream)
            {
                this.Notify.Icon = new System.Drawing.Icon(iconStream);
            }
            this.Notify.Visible = false;
        }

    }
}
