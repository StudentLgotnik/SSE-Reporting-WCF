using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.ViewModel
{
    class InfoViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The context
        /// </summary>
        private DBContext context;
        /// <summary>
        /// The selected employee
        /// </summary>
        private Employee selectedEmployee;

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>
        /// The employee.
        /// </value>
        public String Employee
        {
            get { return selectedEmployee.ToString(); }
            set
            {
                selectedEmployee = new Employee();
                OnPropertyChanged("EmployeeToString");
            }
        }

        /// <summary>
        /// Gets or sets the selected employee.
        /// </summary>
        /// <value>
        /// The selected employee.
        /// </value>
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoViewModel"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="empl">The empl.</param>
        public InfoViewModel(DBContext context, Employee empl)
        {
            this.context = context;
            selectedEmployee = empl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
