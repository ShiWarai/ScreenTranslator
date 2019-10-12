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

namespace ScreenTranslator_MainApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResourceManager LanguageResource;
        public MainWindow()
        {
            InitializeComponent();
            this.LanguageResource = new ResourceManager("ScreenTranslator_MainApp.Lang.lang", Assembly.GetExecutingAssembly());
            Title = LanguageResource.GetString("Title");
        }
    }
}
