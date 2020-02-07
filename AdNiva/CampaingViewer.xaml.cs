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

namespace AdNiva
{
    /// <summary>
    /// Логика взаимодействия для CampaingViewer.xaml
    /// </summary>
    public partial class CampaingViewer : UserControl
    {
        public bool IsChosen;
        public bool check;
        public double strokeThin;
        public int id;
        private static Color titleColor = new Color { R = 44, B = 44, G = 44, A = 255 };
        public delegate void CampCLick(int id);
        public event CampCLick Click;
        
        public CampaingViewer(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsChosen)
            {
                TitleBackground.Fill = new SolidColorBrush(new Color { R = 56, G = 112, B = 109, A = 255 });
            }
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsChosen)
            {
                TitleBackground.Fill = new SolidColorBrush(titleColor);
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            check = true;
            TitleBackground.Fill = new SolidColorBrush(new Color { R = 56, G = 112, B = 109, A = 255 });
            TitleBackground.Stroke = new SolidColorBrush(new Color { R = 55, G = 183, B = 174, A = 255 });
            TitleBackground.StrokeThickness = 3;
            Click(this.id);
        }
        public void SetStatus(int status)
        {
            switch (status)
            {
                case 0:
                    Indicator.Fill = new SolidColorBrush(Colors.Red);
                    Indicator.ToolTip = "Остановлена";
                    break;
                case 1:
                    Indicator.Fill = new SolidColorBrush(Colors.Green);
                    Indicator.ToolTip = "Запущена";
                    break;
                case 2:
                    Indicator.Fill = new SolidColorBrush(Colors.Blue);
                    break;
            }

        }
    }
}
