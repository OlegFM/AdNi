using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdNiva
{
    /// <summary>
    /// Логика взаимодействия для UserSettings.xaml
    /// </summary>
    public partial class UserSettings : Window
    {
        public delegate void AppSettings();
        public event AppSettings Save;
        public UserSettings()
        {
            InitializeComponent();
            AdCabinetID.Text = Properties.Settings.Default.AdCabinetID;
            
        }

        private void SavePropertys_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AdCabinetID = AdCabinetID.Text;
            Properties.Settings.Default.Save();
            Save();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            System.IO.Directory.Delete(Path, true);
            try
            {
                System.IO.Directory.Delete(Path, true);
            }
            catch (Exception)
            {
            }
            MainWindow Login = new MainWindow();
            Login.Show();
            this.Close();
            Application.Current.Dispatcher.Invoke(() =>
            {
                Maiven window3 = Application.Current.Windows.OfType<Maiven>().FirstOrDefault();
                if (window3 != null)
                    window3.Close();
            });

        }
    }
}
