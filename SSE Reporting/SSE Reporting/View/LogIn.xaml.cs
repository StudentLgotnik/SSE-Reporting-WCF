using SSE_Reporting.Model;
using SSE_Reporting.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SSE_Reporting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            DBContext context = new DBContext();
            InitializeComponent();
            LogInViewModel lvm = new LogInViewModel(context);
            DataContext = lvm;
            //if (lvm.Close == null)
            //    lvm.Close = new Action(this.Close);
        }
    }
}
