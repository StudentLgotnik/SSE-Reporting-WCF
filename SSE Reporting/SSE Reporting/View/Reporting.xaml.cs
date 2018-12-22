using SSE_Reporting.Model;
using SSE_Reporting.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для Reporting.xaml
    /// </summary>
    public partial class Reporting : Window
    {
        public Reporting(DBContext context, Employee empl)
        {
            InitializeComponent();

            ReportingViewModel rvm = new ReportingViewModel(context,empl);
            DataContext = rvm;
            if (empl.Role == Role.User)
            {
                AdminBtn.IsHitTestVisible = false;
            }
            if (rvm.Close == null)
                rvm.Close = new Action(this.Close);
        }
    }
}
