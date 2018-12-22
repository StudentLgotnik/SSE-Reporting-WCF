using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using SSE_Reporting.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SSE_Reporting.ViewModel
{
    class LogInViewModel : INotifyPropertyChanged, IDisposable
    {

        /// <summary>
        /// The context
        /// </summary>
        DBContext context;
        /// <summary>
        /// The employee repo
        /// </summary>
        IRepository<Employee> employeeRepo;
        //public ObservableCollection<Employee> Employees { get; set; }        
        /// <summary>
        /// The log in
        /// </summary>
        private RelayCommand logIn;
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
        /// Gets the log in.
        /// </summary>
        /// <value>
        /// The log in.
        /// </value>
        public RelayCommand LogIn
        {
            get
            {
                return logIn ??
                    (logIn = new RelayCommand(obj =>
                    {
                        var pass = obj as PasswordBox;
                        Employee employee = null;

                        if (!Employee.Login.Equals("") && pass != null)
                        {
                            foreach (Employee empl in employeeRepo.getAll())
                            {
                                if (empl.Login == Employee.Login && empl.Password == pass.Password)
                                {
                                    employee = empl;
                                    Employee.Login = "";
                                    pass.Password = "";
                                    Reporting reporting = new Reporting(context, empl);
                                    reporting.ShowDialog();
                                    //Close();
                                }
                                int id = empl.Id;
                                string login = empl.Login;
                                string password = empl.Password;
                            }
                            if (employee == null)
                            {
                                MessageBox.Show("Incorrect username or password.");
                                pass.Password = "";
                            }
                        }
                    }));
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
                        SignUp signup = new SignUp(context);
                        signup.ShowDialog();
                        //Close();
                    }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInViewModel"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LogInViewModel(DBContext context)
        {
            this.context = context;
            employeeRepo = new EmployeeImpl(context);
            //Employees = new ObservableCollection<Employee>(employeeRepo.getAll());
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
