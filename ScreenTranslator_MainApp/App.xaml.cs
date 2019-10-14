using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ScreenTranslator_MainApp.ViewModel;

namespace ScreenTranslator_MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        AppManager Controller;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Controller = new AppManager();

            await Controller.ShowWindow();

            Shutdown();
        }
    }
}
