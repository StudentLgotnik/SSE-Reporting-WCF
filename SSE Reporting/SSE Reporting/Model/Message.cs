using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class Message : INotifyPropertyChanged
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private int id;
        /// <summary>
        /// The message
        /// </summary>
        private string message;
        /// <summary>
        /// The employee identifier
        /// </summary>
        private int? employee_id;
        /// <summary>
        /// The date
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
            id = 0;
            this.message = "";
            employee_id = 0;
            date = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public Message(Employee employee)
        {
            id = 0;
            this.message = "";
            employee_id = employee.Id;
            date = DateTime.Now;
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
                OnPropertyChanged("MessageId");
            }
        }

        /// <summary>
        /// Gets or sets the messagee.
        /// </summary>
        /// <value>
        /// The messagee.
        /// </value>
        public string Messagee
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Messagee");
            }
        }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int? EmployeeId
        {
            get { return employee_id; }
            set
            {
                employee_id = value;
                OnPropertyChanged("EmployeeId");
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, args);
            }
        }
    }
}
