using System;
using System.Collections.Generic;
using System.IO;
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
using System.Net;

namespace AdNiva
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        public AuthForm()
        {
            InitializeComponent();
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetToken.Navigate("https://oauth.vk.com/authorize?client_id=5947030&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=ads&response_type=token&v=5.52");
            
        }

        private void GetToken_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if(e.Uri.ToString().Contains("access_token="))
            {
                GetUserToken(e.Uri.ToString());
            }
        }
        private void GetUserToken(string uri)
        {
            char[] Symbols = { '=', '&' };
            string[] URL = uri.Split(Symbols);
            Properties.Settings.Default.AccessToken = URL[1];
            Properties.Settings.Default.UserID = URL[5];
            Properties.Settings.Default.Save();
            this.Close();
          
        }

        private void GetToken_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
