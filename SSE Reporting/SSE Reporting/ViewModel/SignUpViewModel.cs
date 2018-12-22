using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using SSE_Reporting.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SSE_Reporting.ViewModel
{
    class SignUpViewModel : INotifyPropertyChanged, IDisposable
    {
        private DBContext context;

        IRepository<Employee> employeeRepo;

        /// <summary>
        /// The sign up
        /// </summary>
        private RelayCommand signUp;
        /// <summary>
        /// The employee
        /// </summary>
        private Employee employee;

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>
        /// The employee.
        /// </value>
        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        /// <summary>
        /// Gets the sign up.
        /// </summary>
        /// <value>
        /// The sign up.
        /// </value>
        public RelayCommand SignUp
        {
            get
            {
                return signUp ??
                    (signUp = new RelayCommand(obj =>
                    {
                        var pass = obj as PasswordBox;
                        bool contains = false;
                        foreach (Employee empl in employeeRepo.getAll())
                        {
                            if (empl.Login == Employee.Login)
                            {
                                MessageBox.Show("Login is already taken! Please log in or choose another.");
                                Employee.Login = "";
                                pass.Password = "";
                                contains = true;
                            }
                        }
                        if (contains == false)
                        {
                            Reporting  reporting = new Reporting(context ,employeeRepo.save(new Employee(Employee.Login, pass.Password)));
                            reporting.ShowDialog();
                            //Close();
                        }
                    }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpViewModel"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SignUpViewModel(DBContext context)
        {
            employeeRepo = new EmployeeImpl(context);
            this.context = context;
            Employee = new Employee();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (employeeRepo != null)
                {
                    employeeRepo.Dispose();
                    employeeRepo = null;
                }
            }
        }

        public Action Close { get; set; }
    }
}
