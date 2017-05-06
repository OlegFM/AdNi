using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.HockeyApp;

namespace AdNiva
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
          //  HockeyClient.Current.Configure("8494df31ede646ebbf86ca08783cbaba");
          //  await HockeyClient.Current.SendCrashesAsync(true);
        }
    }
}
