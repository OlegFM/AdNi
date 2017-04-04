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
            if (!Focusable)
            {
                TitleBackground.Fill = new SolidColorBrush(Colors.DarkBlue);
            }
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!Focusable)
            {
                TitleBackground.Fill = new SolidColorBrush(titleColor);
            }
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            TitleBackground.Fill = new SolidColorBrush(titleColor);
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Click(this.id);
        }
    }
}
