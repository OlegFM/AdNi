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
        VkAPI API = new VkAPI(Properties.Settings.Default.AccessToken);
        public Maiven()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            
            //Запрос фотографии и имени пользователя
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
        }

        private void UserIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserSettings window = new UserSettings();
            window.Save += LoadCampaigns;
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
            GetCampaingResponse add = API.GetCampaing(response.response[0].id);
            CampaingViewer viewer = new CampaingViewer(add.response[0].id);
            viewer.Click += Choose_Camp;
            viewer.campaingTitle.Content = add.response[0].name;
            viewer.DayLim.Content = add.response[0].day_limit;
            viewer.AllLim.Content = add.response[0].all_limit;
            CampaingStack.Children.Add(viewer);
        }
        private void Choose_Camp(int id)
        {
            AdsColumn.Children.Remove(start_ads);
            if(AdsStack.Children.Count > 0)
            {
                AdsStack.Children.RemoveRange(0, AdsStack.Children.Count);
            }
            GetAdsResponse ads = API.GetAds(id.ToString());
            if(ads.response != null)
            {
                foreach (GetAdsResponseBody ad in ads.response)
                {
                    AdViewer viewer = new AdViewer(ad.id);
                    viewer.AdTitle.Content = ad.name;
                    viewer.CPM.Content = ad.cpm;
                    viewer.Limit.Content = ad.all_limit;
                    viewer.Chouse += Ad_Chouse;
                    AdsStack.Children.Add(viewer);
                }
            }
            foreach (CampaingViewer ad in CampaingStack.Children)
            {
                if (ad.IsChosen)
                {
                    ad.IsChosen = false;
                    ad.TitleBackground.Fill = new SolidColorBrush(new Color { R = 44, B = 44, G = 44, A = 255 });
                    ad.TitleBackground.Stroke = new SolidColorBrush(Colors.Black);
                    ad.TitleBackground.StrokeThickness = ad.strokeThin;
                }
                if (ad.check)
                {
                    ad.IsChosen = true;
                    ad.check = false;
                }
            }
        }

        private void Ad_Chouse()
        {
            foreach (AdViewer ad in AdsStack.Children)
            {
                if (ad.IsChosen)
                {
                    ad.IsChosen = false;
                    ad.TitleBackground.Fill = new SolidColorBrush(new Color { R = 44, B = 44, G = 44, A = 255 });
                    ad.TitleBackground.Stroke = new SolidColorBrush(Colors.Black);
                    ad.TitleBackground.StrokeThickness = ad.strokeThin;
                }
                if (ad.check)
                {
                    ad.IsChosen = true;
                    ad.check = false;
                }
            }
            
        }

        private void LoadCampaigns() {
            //Получение кампаний из кабинета
            if (CampaingStack.Children.Count > 0)
            {
                CampaingStack.Children.RemoveRange(0, CampaingStack.Children.Count - 1);
            }
            API = new VkAPI(Properties.Settings.Default.AccessToken);
            Refresh_Budget();
            GetCampaingResponse load = API.GetCampaings();
            if (load.response != null)
            {
                for (int i = 0; i < load.response.Count; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    CampaingViewer viewer = new CampaingViewer(load.response[i].id);
                    GetAdsResponse ads = API.GetAds(load.response[i].id.ToString());
                    viewer.Click += Choose_Camp;
                    viewer.campaingTitle.Content = load.response[i].name;
                    viewer.DayLim.Content = load.response[i].day_limit;
                    viewer.AllLim.Content = load.response[i].all_limit;
                    if (ads.response == null)
                    {
                        viewer.AdsCount.Content = "err:"+ads.error.error_code;
                    }
                    else
                    {
                        viewer.AdsCount.Content = ads.response.Count;
                    }
                        CampaingStack.Children.Add(viewer);
                }
            }
        }
        private void Refresh_Budget()
        {
            Budget.Content = API.GetBudget();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCampaigns();
        }
    }
}
