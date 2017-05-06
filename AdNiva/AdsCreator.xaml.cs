using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /// Логика взаимодействия для AdsCreator.xaml
    /// </summary>
    public partial class AdsCreator : Window
    {
        VkAPI API = new VkAPI(Properties.Settings.Default.AccessToken);
        GetCategoriesResponse categories = new GetCategoriesResponse();
        int category1_id;
        int category2_id;
        public AdsCreator()
        {
            InitializeComponent();
            categories = API.GetCategories();
            foreach (GetCategoriesBody element in categories.response)
            {
                Label title = new Label();
                title.Content = element.name;
                title.FontWeight = FontWeights.Heavy;
                Label title1 = new Label();
                title1.Content = element.name;
                title1.FontWeight = FontWeights.Heavy;
                Category1.Items.Add(title);
                Category2.Items.Add(title1);
                if (element.subcategories != null)
                {
                    foreach (GetCategoriesSub item in element.subcategories)
                    {
                        Category1.Items.Add(item.name);
                        Category2.Items.Add(item.name);
                    }
                    
                }
                Category2.Items.Add(new Separator());
                Category1.Items.Add(new Separator());
            }
        }
        // Технические моменты (анимация, управление окном)
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            
        }

        private void Scroller_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }

        private void CloseButon_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButon.BorderBrush = new SolidColorBrush(Colors.WhiteSmoke);
        }

        private void CloseButon_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButon.BorderBrush = new SolidColorBrush(Colors.White);
        }

        private void CloseButon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        

        //Работа с данными
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dynamic Ad = new ExpandoObject();
            Ad.name = AdName.Text;

            switch (Ad_Format.SelectedIndex)
            {
                case 0:
                    Ad.ad_format = 1;
                    break;
                case 1:
                    Ad.ad_format = 2;
                    break;
                case 2:
                    Ad.ad_format = 4;
                    break;
                case 3:
                    Ad.ad_format = 8;
                    break;
                case 4:
                    Ad.ad_format = 9;
                    break;
            }
            if(Cpc.IsChecked == true)
            {
                Ad.cost_type = 0;
                Ad.cpc = Double.Parse(Cost.Text);                
            }
            else
            {
                Ad.cost_type = 1;
                Ad.cpm = Double.Parse(Cost.Text);
            }
            Ad.all_limit = Double.Parse(AllLimit.Text);

            switch (AdPlatform.SelectedIndex)
            {
                case 0:
                    if (Ad.cost_type == 0 && Ad.ad_format == 1)
                    {
                        Ad.ad_platform = 0;
                    }
                    break;
                case 1:
                    if (Ad.cost_type == 0 && Ad.ad_format == 1)
                    {
                        Ad.ad_platform = 1;
                    }
                    break;
                case 2:
                    if (Ad.ad_format == 9)
                    {
                        Ad.ad_platform = "all";
                    }
                    break;
                case 3:
                    if (Ad.ad_format == 9)
                    {
                        Ad.ad_platform = "desktop";
                    }
                    break;
                case 4:
                    if (Ad.ad_format == 9)
                    {
                        Ad.ad_platform = "mobile";
                    }
                    break;
            }
            // работа с категориями
            if (categories.response.Find(x => x.name == Category1.SelectedValue.ToString()) != null)
            {
                GetCategoriesBody category1 = categories.response.Find(x => x.name == Category1.SelectedValue.ToString());
                Ad.category1_id = category1.id;
            }
            else
            {
                foreach (GetCategoriesBody item in categories.response)
                {
                    GetCategoriesSub category1 = item.subcategories.Find(x => x.name == Category1.SelectedValue.ToString());
                    Ad.category1_id = category1_id;
                }
            }
            if (Category2.SelectedItem != null)
            {
                if (categories.response.Find(x => x.name == Category2.SelectedValue.ToString()) != null)
                {
                    GetCategoriesBody category1 = categories.response.Find(x => x.name == Category1.SelectedValue.ToString());
                    Ad.category2_id = category1.id;
                }
                else
                {
                    foreach (GetCategoriesBody item in categories.response)
                    {
                        GetCategoriesSub category1 = item.subcategories.Find(x => x.name == Category2.SelectedValue.ToString());
                        Ad.category2_id = category1.id;
                    }
                }
            }
            //***************************************************************************************************************
            
            
        }
    }
}
