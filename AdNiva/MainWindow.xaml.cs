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
using System.Windows.Navigation;
using System.Windows.Shapes;
using xNet;
using Newtonsoft.Json;

namespace AdNiva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpRequest CheckToken = new HttpRequest();
            CheckToken.AddUrlParam("access_token", Properties.Settings.Default.AccessToken);
            CheckToken.AddUrlParam("v", "5.63");
            string Result = CheckToken.Get("https://api.vk.com/method/account.setOffline").ToString();
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            try
            {
                   Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            }
            catch
            {
                Dict.Add("response", "0");
            }
                if (Dict["response"] != "1")
            {
                AuthForm GetToken = new AuthForm();
                GetToken.ShowDialog();
            }
            Maiven open = new Maiven();
            open.Show();
            this.Close();
        }
    }
}
