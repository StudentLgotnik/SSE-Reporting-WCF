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
    public class Report : INotifyPropertyChanged
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private int id;
        /// <summary>
        /// The project
        /// </summary>
        private Project project;
        /// <summary>
        /// The task
        /// </summary>
        private Task task;
        /// <summary>
        /// The employee identifier
        /// </summary>
        private int? employee_id;
        /// <summary>
        /// The activity
        /// </summary>
        private Activity activity;
        /// <summary>
        /// The start hours
        /// </summary>
        private TimeSpan startHours;
        /// <summary>
        /// The end hours
        /// </summary>
        private TimeSpan endHours;
        /// <summary>
        /// The date
        /// </summary>
        private DateTime date;
        /// <summary>
        /// The comment
        /// </summary>
        private string comment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        public Report()
        {
            id = 0;
            project = null;
            task = null;
            startHours =
            endHours = new TimeSpan();
            date = new DateTime();
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
                OnPropertyChanged("ReportId");
            }
        }
        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project Project
        {
            get { return project; }
            set
            {
                project = value;
                OnPropertyChanged("Project");
            }
        }
        /// <summary>
        /// Gets or sets the task.
        /// </summary>
        /// <value>
        /// The task.
        /// </value>
        public Task Task
        {
            get { return task; }
            set
            {
                task = value;
                OnPropertyChanged("Task");
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
        /// Gets or sets the activity.
        /// </summary>
        /// <value>
        /// The activity.
        /// </value>
        public Activity Activity
        {
            get { return activity; }
            set
            {
                activity = value;
                OnPropertyChanged("ProjectActivity");
            }
        }
        /// <summary>
        /// Gets or sets the start hours.
        /// </summary>
        /// <value>
        /// The start hours.
        /// </value>
        public TimeSpan StartHours
        {
            get { return startHours; }
            set
            {
                startHours = value;
                OnPropertyChanged("StartHours");
            }
        }
        /// <summary>
        /// Gets or sets the end hours.
        /// </summary>
        /// <value>
        /// The end hours.
        /// </value>
        public TimeSpan EndHours
        {
            get { return endHours; }
            set
            {
                endHours = value;
                OnPropertyChanged("EndHours");
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

        /// <summary>
        /// Gets or sets the commect.
        /// </summary>
        /// <value>
        /// The commect.
        /// </value>
        public string Commect
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged("Date");
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
            IRepository<Employee> ir = new EmployeeImpl(new DBContext());
            Employee empl = ir.get((int)EmployeeId);
            return String.Format("{0} ({1})   {2}   {3}[{4} - {5}]", Project, empl, Task, Date.ToString("dd/MM/yyyy"), StartHours, EndHours);
        }

        /// <summary>
        /// Called when [property changed].
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
