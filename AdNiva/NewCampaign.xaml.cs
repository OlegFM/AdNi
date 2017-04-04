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
    /// Логика взаимодействия для NewCampaign.xaml
    /// </summary>
    public partial class NewCampaign : Window
    {
        public delegate void Response(CreateCampaingResponse result);
        public event Response Create;
        VkAPI API = new VkAPI(Properties.Settings.Default.AccessToken);
        CreateCampaignDataValidate data;
        public NewCampaign()
        {
            data = new CreateCampaignDataValidate();
            InitializeComponent();
            this.DataContext = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateCampaignData[] Data = new CreateCampaignData[1];
            Data[0] = new CreateCampaignData();
            Data[0].name = Name.Text;
            Data[0].status = Status.IsChecked.Value;
            Data[0].start_time = new Helper().GetUnixTime(StartDate.Text, start_time_hour.Text, start_time_minute.Text).ToString();
            Data[0].stop_time = new Helper().GetUnixTime(StopDate.Text, stop_time_hour.Text, stop_time_minute.Text).ToString();
            if (CampaingType.SelectedIndex == 0)
            {
                Data[0].type = "normal";
            }
            else
            {
                Data[0].type = "promoted_posts";
            }
            Data[0].day_limit = DayLimit.Text;
            Data[0].all_limit = AllLimit.Text;
            CreateCampaingResponse response = new CreateCampaingResponse();
            response = API.CreateCampaign(Data);
            Create(response);
            this.Close();
        }
        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
        //    if (data.Val.Contains(false))
        //    {
        //        CreateCamp.IsEnabled = false;
        //    }
        //    else
        //    {
        //        CreateCamp.IsEnabled = true;
        //    }
        }
    }
}
