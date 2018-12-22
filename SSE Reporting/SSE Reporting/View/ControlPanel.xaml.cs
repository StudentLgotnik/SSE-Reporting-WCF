using SSE_Reporting.Model;
using SSE_Reporting.ViewModel;
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

namespace SSE_Reporting.View
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class ControlPanel : Window
    {
        public ControlPanel(DBContext context, Employee empl)
        {
            InitializeComponent();

            DataContext = new ControlPanelViewModel(context, empl);
        }

    }
}
