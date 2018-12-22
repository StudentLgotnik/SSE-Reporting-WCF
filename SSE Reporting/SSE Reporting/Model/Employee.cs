using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class Employee : INotifyPropertyChanged
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private int id;
        /// <summary>
        /// The login
        /// </summary>
        private string login;
        /// <summary>
        /// The password
        /// </summary>
        private string password;
        /// <summary>
        /// The time off
        /// </summary>
        private double timeOff;
        /// <summary>
        /// The sickness
        /// </summary>
        private double sickness;
        /// <summary>
        /// The project identifier
        /// </summary>
        private int? project_id;
        /// <summary>
        /// The role
        /// </summary>
        private Role role;

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        public Employee()
        {
            id = 0;
            login =
            password = null;
            timeOff =
            sickness = 0;
            project_id = null;
            role = Role.User;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        public Employee(string login, string password)
        {
            id = 0;
            this.login = login;
            this.password = password;
            timeOff = 0;
            project_id = null;
            sickness = 20;
            role = Role.User;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("EmployeeId");
            }
        }
        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        /// <summary>
        /// Gets or sets the time off.
        /// </summary>
        /// <value>
        /// The time off.
        /// </value>
        public double TimeOff
        {
            get { return timeOff; }
            set
            {
                timeOff = value;
                OnPropertyChanged("TimeOff");
            }
        }
        /// <summary>
        /// Gets or sets the sickness.
        /// </summary>
        /// <value>
        /// The sickness.
        /// </value>
        public double Sickness
        {
            get { return sickness; }
            set
            {
                sickness = value;
                OnPropertyChanged("Sickness");
            }
        }
        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public int? ProjectId
        {
            get { return project_id; }
            set
            {
                project_id = value;
                OnPropertyChanged("Projects");
            }
        }
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Role Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{0}", Login);
        }
        /// <summary>
        /// Called when /[property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var employee = obj as Employee;
            return employee != null &&
                   id == employee.id &&
                   login == employee.login &&
                   password == employee.password &&
                   timeOff == employee.timeOff &&
                   sickness == employee.sickness &&
                   project_id == employee.project_id &&
                   role == employee.role;
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 1713803354;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(login);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            hashCode = hashCode * -1521134295 + timeOff.GetHashCode();
            hashCode = hashCode * -1521134295 + sickness.GetHashCode();
            hashCode = hashCode * -1521134295 + project_id.GetHashCode();
            hashCode = hashCode * -1521134295 + role.GetHashCode();
            return hashCode;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
