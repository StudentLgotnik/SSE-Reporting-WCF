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

namespace SSE_Reporting
{
    /// <summary>
    /// Логика взаимодействия для Slider.xaml
    /// </summary>
    public partial class Slider : UserControl
    {
        public Slider()
        {
            InitializeComponent();
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(double), typeof(Slider),
                new PropertyMetadata(0.0, (o, args) => ((Slider)o).UpdateValues()));

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum", typeof(double), typeof(Slider),
                new PropertyMetadata(24.0, (o, args) => ((Slider)o).UpdateValues()));

        void UpdateValues()
        {
            var ratio = Maximum <= 0 ? 0.0 : Value / Maximum;
            if (ratio < 0)
                ratio = 0;
            if (ratio > 1)
                ratio = 1;
            LeftColumn.Width = new GridLength(ratio, GridUnitType.Star);
            RightColumn.Width = new GridLength(1 - ratio, GridUnitType.Star);
        }
    }
}
