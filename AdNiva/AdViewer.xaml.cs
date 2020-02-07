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
    /// Логика взаимодействия для AdViewer.xaml
    /// </summary>
    public partial class AdViewer : UserControl
    {
        public bool IsChosen;
        public bool check;
        public double strokeThin;
        public int id;
        public delegate void ChouseEvent();
        public event ChouseEvent Chouse;
        private static Color titleColor = new Color { R = 44, B = 44, G = 44, A = 255 };
        public AdViewer(int id)
        {
            InitializeComponent();
            this.id = id;
            strokeThin = TitleBackground.StrokeThickness;
        }
        private void Viewer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsChosen)
            {
                TitleBackground.Fill = new SolidColorBrush(new Color { R = 56, G = 112, B = 109, A = 255 });

            }
        }
        private void Viewer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsChosen)
            {
                TitleBackground.Fill = new SolidColorBrush(titleColor);
            }
        }

        private void Viewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            check = true;
            TitleBackground.Fill = new SolidColorBrush(new Color { R = 56, G = 112, B = 109, A = 255 });
            TitleBackground.Stroke = new SolidColorBrush(new Color { R = 55, G = 183, B = 174, A = 255 });
            TitleBackground.StrokeThickness = 3;
            Chouse();
        }

        private void SetStatus(int status)
        {
            switch (status)
            {
                case 0:
                    Indicator.Fill = new SolidColorBrush(Colors.Red);
                    Indicator.ToolTip = "Остановлено";
                    break;
                case 1:
                    Indicator.Fill = new SolidColorBrush(Colors.Green);
                    Indicator.ToolTip = "Запущено";
                    break;
                case 2:
                    Indicator.Fill = new SolidColorBrush(Colors.Blue);
                    break;
            }
        }
    }
}
