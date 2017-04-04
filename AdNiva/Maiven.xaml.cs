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
using xNet;
using System.Net;

namespace AdNiva
{
    /// <summary>
    /// Логика взаимодействия для Maiven.xaml
    /// </summary>
    public partial class Maiven : Window
    {
        Label start_ads = new Label();
        Label start_info = new Label();
        public Maiven()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            
            //Запрос фотографии и имени пользователя
            VkAPI API = new VkAPI(Properties.Settings.Default.AccessToken);
            Dictionary<string, string> UserInfo = API.GetUserInfo();
            UserName.Content = UserInfo["first_name"] + " " + UserInfo["last_name"];
            WebClient Downloader = new WebClient();
            try
            {
                Downloader.DownloadFile(UserInfo["photo_100"], "Resources/" + System.IO.Path.GetFileName(UserInfo["photo_100"]));
                Downloader.Dispose();
            }
            catch { }

            BitmapImage Icon = new BitmapImage(new Uri("Resources/" + System.IO.Path.GetFileName(UserInfo["photo_100"]), UriKind.Relative)) ;
            ImageBrush I = new ImageBrush(Icon);
            UserIcon.Fill = I;

            //Формирование колонок
            double Width = SystemParameters.VirtualScreenWidth / 3;
            CampaingColumn.Width =  Width-10-Width/4;
            CampaingScroll.Width = CampaingColumn.Width;
            CampaingStack.Width = CampaingColumn.Width;
            AdsColumn.Margin = new Thickness(CampaingColumn.Width+15, 100, 0, 10);
            AdsColumn.Width = Width - 5 + Width/8;
            AdsScroll.Width = AdsColumn.Width;
            AdsStack.Width = AdsColumn.Width;
            AdsStack.Height = AdsColumn.Height;
            start_ads.Content = "Выберите кампанию. Здесь будут отображаться все объявления этой кампании";
            start_ads.FontStyle = FontStyles.Italic;
            start_ads.Foreground = new SolidColorBrush(Colors.Azure);
            start_ads.FontWeight = FontWeights.Thin;
            start_ads.Width = AdsColumn.Width;
            start_ads.Height = AdsColumn.Height;
            start_ads.HorizontalContentAlignment = HorizontalAlignment.Center;
            start_ads.VerticalAlignment = VerticalAlignment.Center;
            AdsColumn.Children.Add(start_ads);

            InformationColumn.Margin = new Thickness(CampaingColumn.Width + 15 + AdsColumn.Width+5,100,10,10);
            InformationColumn.Width = Width - 10+Width/8;
            start_info.Content = "Здесь будет отображаться информация о выбранном объявлении\n  или выбранной кампании, если не выбрано ни одно объявление";
            start_info.Width = InformationColumn.Width;
            start_info.Height = InformationColumn.Height;
            start_info.HorizontalContentAlignment = HorizontalAlignment.Center;
            start_info.VerticalAlignment = VerticalAlignment.Center;
            start_info.FontStyle = FontStyles.Italic;
            start_info.FontWeight = FontWeights.Thin;
            start_info.Foreground = new SolidColorBrush(Colors.Azure);
            InformationColumn.Children.Add(start_info);

            //Получение общей статистики
            Budget.Content = API.GetBudget().ToString();
        }

        private void UserIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserSettings window = new UserSettings();
            window.Show();
        }

        private void Close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void Roll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewCampaign window = new NewCampaign();
            window.Create += Add_Campaing;
            window.Show();
        }
        private void Add_Campaing(CreateCampaingResponse response)
        {
            
            for (int i = 0; i < 10; i++)
            {
                CampaingViewer viewer = new CampaingViewer(i+1);
                viewer.Click += Choose_Camp;
                viewer.campaingTitle.Content = "Привет, Андрей #" + i.ToString();
                CampaingStack.Children.Add(viewer);
            }
        }
        private void Choose_Camp(int id)
        {
            start_ads.Content = id.ToString();
        }
    }
}
