using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace SSE_Reporting.ViewModel
{
    class ControlPanelViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// The selected employee
        /// </summary>
        private Employee selectedEmployee;
        /// <summary>
        /// The employee selected index
        /// </summary>
        private int employeeSelectedIndex;
        /// <summary>
        /// The selected admin
        /// </summary>
        private Employee selectedAdmin;
        /// <summary>
        /// The admin selected index
        /// </summary>
        private int adminSelectedIndex;
        /// <summary>
        /// The selected project
        /// </summary>
        private Project selectedProject;
        /// <summary>
        /// The project selected index
        /// </summary>
        private int projectSelectedIndex;
        /// <summary>
        /// The selected task
        /// </summary>
        private Task selectedTask;
        /// <summary>
        /// The task selected index
        /// </summary>
        private int taskSelectedIndex;
        /// <summary>
        /// The employees
        /// </summary>
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        /// <summary>
        /// The admins
        /// </summary>
        private ObservableCollection<Employee> admins = new ObservableCollection<Employee>();
        /// <summary>
        /// The projects
        /// </summary>
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        /// <summary>
        /// The tasks
        /// </summary>
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public ObservableCollection<Employee> Employees {
            get { return employees; }
            set {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }
        /// <summary>
        /// Gets or sets the admins.
        /// </summary>
        /// <value>
        /// The admins.
        /// </value>
        public ObservableCollection<Employee> Admins {
            get { return admins; }
            set {
                admins = value;
                OnPropertyChanged("Admins");
            }
        }
        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public ObservableCollection<Project> Projects {
            get { return projects; }
            set {
                projects = value;
                OnPropertyChanged("Projects");
            }
        }
        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>
        /// The tasks.
        /// </value>
        public ObservableCollection<Task> Tasks {
            get { return tasks; }
            set {
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        /// <summary>
        /// Gets or sets the index of the project selected.
        /// </summary>
        /// <value>
        /// The index of the project selected.
        /// </value>
        public int ProjectSelectedIndex
        {
            get { return projectSelectedIndex; }
            set { projectSelectedIndex = value; OnPropertyChanged("ProjectSelectedIndex"); }
        }

        /// <summary>
        /// Gets or sets the index of the task selected.
        /// </summary>
        /// <value>
        /// The index of the task selected.
        /// </value>
        public int TaskSelectedIndex
        {
            get { return taskSelectedIndex; }
            set { taskSelectedIndex = value; OnPropertyChanged("TaskSelectedIndex"); }
        }

        /// <summary>
        /// Gets or sets the index of the employee selected.
        /// </summary>
        /// <value>
        /// The index of the employee selected.
        /// </value>
        public int EmployeeSelectedIndex
        {
            get { return employeeSelectedIndex; }
            set { employeeSelectedIndex = value; OnPropertyChanged("EmployeeSelectedIndex"); }
        }

        /// <summary>
        /// Gets or sets the index of the admin selected.
        /// </summary>
        /// <value>
        /// The index of the admin selected.
        /// </value>
        public int AdminSelectedIndex
        {
            get { return adminSelectedIndex; }
            set { adminSelectedIndex = value; OnPropertyChanged("AdminSelectedIndex"); }
        }

        /// <summary>
        /// The delete project
        /// </summary>
        private RelayCommand deleteProject;
        /// <summary>
        /// The add project
        /// </summary>
        private RelayCommand addProject;
        /// <summary>
        /// The edit project
        /// </summary>
        private RelayCommand editProject;
        /// <summary>
        /// The add task
        /// </summary>
        private RelayCommand addTask;
        /// <summary>
        /// The delete task
        /// </summary>
        private RelayCommand deleteTask;
        /// <summary>
        /// The task to project
        /// </summary>
        private RelayCommand taskToProject;
        /// <summary>
        /// The add employee
        /// </summary>
        private RelayCommand addEmployee;
        /// <summary>
        /// The delete employee
        /// </summary>
        private RelayCommand deleteEmployee;
        /// <summary>
        /// The make admin
        /// </summary>
        private RelayCommand makeAdmin;
        /// <summary>
        /// The employee to project
        /// </summary>
        private RelayCommand employeeToProject;
        /// <summary>
        /// The add admin
        /// </summary>
        private RelayCommand addAdmin;
        /// <summary>
        /// The delete admin
        /// </summary>
        private RelayCommand deleteAdmin;
        /// <summary>
        /// The pick up admin
        /// </summary>
        private RelayCommand pickUpAdmin;
        /// <summary>
        /// The admin to project
        /// </summary>
        private RelayCommand adminToProject;

        /// <summary>
        /// The database context
        /// </summary>
        private DBContext dbContext;

        /// <summary>
        /// The employee repo
        /// </summary>
        IRepository<Employee> employeeRepo;
        //IRepository<Admin> adminRepo;        
        /// <summary>
        /// The project repo
        /// </summary>
        IRepository<Project> projectRepo;
        /// <summary>
        /// The report repo
        /// </summary>
        IRepository<Report> reportRepo;
        /// <summary>
        /// The task repo
        /// </summary>
        IRepository<Task> taskRepo;

        /// <summary>
        /// Gets the add admin.
        /// </summary>
        /// <value>
        /// The add admin.
        /// </value>
        public RelayCommand AddAdmin
        {
            get
            {
                return addAdmin ??
                    (addAdmin = new RelayCommand(obj =>
                    {
                        Employee admin = employeeRepo.save(new Admin { Login = SelectedAdmin.Login, Password = SelectedAdmin.Password });
                        Admins.Add(admin);
                        SelectedAdmin = new Employee();
                    }));
            }
        }

        /// <summary>
        /// Gets the delete admin.
        /// </summary>
        /// <value>
        /// The delete admin.
        /// </value>
        public RelayCommand DeleteAdmin
        {
            get
            {
                return deleteAdmin ??
                    (deleteAdmin = new RelayCommand(obj =>
                    {
                        if (!(SelectedAdmin.Id == 0))
                        {
                            Employee admin = employeeRepo.delete(SelectedAdmin);
                            Admins.Remove(SelectedAdmin);
                            SelectedAdmin = new Employee();
                        }
                    }));
            }
        }

        /// <summary>
        /// Gets the add employee.
        /// </summary>
        /// <value>
        /// The add employee.
        /// </value>
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                    (addEmployee = new RelayCommand(obj =>
                    {
                        Employee employee = employeeRepo.save(new Employee { Login = SelectedEmployee.Login, Password = SelectedEmployee.Password });
                        Employees.Add(employee);
                        SelectedEmployee = new Employee();
                    }));
            }
        }

        /// <summary>
        /// Gets the delete employee.
        /// </summary>
        /// <value>
        /// The delete employee.
        /// </value>
        public RelayCommand DeleteEmployee
        {
            get
            {
                return deleteEmployee ??
                    (deleteEmployee = new RelayCommand(obj =>
                    {
                        if (!SelectedEmployee.Equals(new Employee()))
                        {
                            Employee employee = employeeRepo.delete(SelectedEmployee);
                            Employees.Remove(SelectedEmployee);
                            SelectedEmployee = new Employee();
                        }

                    }));
            }
        }

        /// <summary>
        /// Gets the make admin.
        /// </summary>
        /// <value>
        /// The make admin.
        /// </value>
        public RelayCommand MakeAdmin
        {
            get
            {
                return makeAdmin ??
                    (makeAdmin = new RelayCommand(obj =>
                    {
                        if (SelectedEmployee != null && !SelectedEmployee.Equals(new Employee()))
                        {
                            SelectedEmployee.Role = Role.Admin;
                            employeeRepo.update(SelectedEmployee);
                            SelectedEmployee = new Employee();
                            Employees = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.User));
                            Admins = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.Admin));
                            AdminSelectedIndex = -1;
                        }
                        else
                            MessageBox.Show("You didn't choose employee.");
                    }));
            }
        }

        /// <summary>
        /// Gets the pick up admin.
        /// </summary>
        /// <value>
        /// The pick up admin.
        /// </value>
        public RelayCommand PickUpAdmin
        {
            get
            {
                return pickUpAdmin ??
                    (pickUpAdmin = new RelayCommand(obj =>
                    {
                        if (SelectedAdmin != null && !SelectedAdmin.Equals(new Admin()))
                        {
                            SelectedAdmin.Role = Role.User;
                            employeeRepo.update(SelectedAdmin);
                            SelectedAdmin = new Admin();
                            SelectedEmployee = new Employee();
                            Employees = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.User));
                            Admins = new ObservableCollection<Employee>(employeeRepo.getAll().Where(empl => empl.Role == Role.Admin));
                            EmployeeSelectedIndex = -1;
                        }
                        else
                            MessageBox.Show("You didn't choose admin.");
                    }));
            }
        }

        /// <summary>
        /// Gets the admin to project.
        /// </summary>
        /// <value>
        /// The admin to project.
        /// </value>
        public RelayCommand AdminToProject
        {
            get
            {
                return adminToProject ??
                    (adminToProject = new RelayCommand(obj =>
                    {
                        if (SelectedProject != null && SelectedAdmin != null && !(SelectedProject.Id == 0) && !(SelectedAdmin.Id == 0))
                        {
                            SelectedAdmin.ProjectId = SelectedProject.Id;
                            SelectedProject.addEmployee(SelectedAdmin);
                            Employee employee = employeeRepo.update(SelectedAdmin);
                            Project project = projectRepo.update(SelectedProject);

                        }
                        else
                            MessageBox.Show("You didn't choose project or task.");

                    }));
            }
        }

        /// <summary>
        /// Gets the employee to project.
        /// </summary>
        /// <value>
        /// The employee to project.
        /// </value>
        public RelayCommand EmployeeToProject
        {
            get
            {
                return employeeToProject ??
                    (employeeToProject = new RelayCommand(obj =>
                    {
                        if (SelectedProject != null && SelectedEmployee != null && !(SelectedProject.Id == 0) && !(SelectedEmployee.Id == 0))
                        {
                            SelectedEmployee.ProjectId = SelectedProject.Id;
                            SelectedProject.addEmployee(selectedEmployee);
                            Employee employee = employeeRepo.update(SelectedEmployee);
                            Project project = projectRepo.update(SelectedProject);
                     }
                        else
                            MessageBox.Show("You didn't choose project or task.");

                    }));
            }
        }

        /// <summary>
        /// Gets the add task.
        /// </summary>
        /// <value>
        /// The add task.
        /// </value>
        public RelayCommand AddTask
        {
            get
            {
                return addTask ??
                    (addTask = new RelayCommand(obj =>
                    {

                        Task task = taskRepo.save(new Task { Name = selectedTask.Name, Activity = selectedTask.Activity });
                        Tasks.Add(task);
                        SelectedTask = new Task();
                    }));
            }
        }

        /// <summary>
        /// Gets the delete task.
        /// </summary>
        /// <value>
        /// The delete task.
        /// </value>
        public RelayCommand DeleteTask
        {
            get
            {
                return deleteTask ??
                    (deleteTask = new RelayCommand(obj =>
                    {
                        if (!SelectedTask.Equals(new Task()))
                        {
                            Task task = taskRepo.delete(SelectedTask);
                            Tasks.Remove(SelectedTask);
                            SelectedTask = new Task();
                        }
                        
                    }));
            }
        }

        /// <summary>
        /// Gets the task to project.
        /// </summary>
        /// <value>
        /// The task to project.
        /// </value>
        public RelayCommand TaskToProject
        {
            get
            {
                return taskToProject ??
                    (taskToProject = new RelayCommand(obj =>
                    {
                        if (SelectedProject != null && SelectedTask != null && !SelectedProject.Equals(new Project()) && !SelectedTask.Equals(new Task()))
                        {
                            SelectedProject.addTask(selectedTask);
                            Project project = projectRepo.update(SelectedProject);
                            
                        }
                        else
                            MessageBox.Show("You didn't choose project or task.");

                    }));
            }
        }

        /// <summary>
        /// Gets the add project.
        /// </summary>
        /// <value>
        /// The add project.
        /// </value>
        public RelayCommand AddProject
        {
            get
            {
                return addProject ??
                    (addProject = new RelayCommand(obj =>
                    {
                                           
                        Project project = projectRepo.save(new Project{ Company = selectedProject.Company, Name = selectedProject.Name });
                        Projects.Add(project);
                        SelectedProject = project;
                    }));
            }
        }

        /// <summary>
        /// Gets the delete project.
        /// </summary>
        /// <value>
        /// The delete project.
        /// </value>
        public RelayCommand DeleteProject
        {
            get
            {
                return deleteProject ??
                    (deleteProject = new RelayCommand(obj =>
                    {
                         Project project = projectRepo.delete(selectedProject);
                         Projects.Remove(SelectedProject);
                         SelectedProject = new Project();
                    }));
            }
        }

        /// <summary>
        /// Gets the edit project.
        /// </summary>
        /// <value>
        /// The edit project.
        /// </value>
        public RelayCommand EditProject
        {
            get
            {
                return editProject ??
                    (editProject = new RelayCommand(obj =>
                    {

                        Project project = projectRepo.update(SelectedProject);
                        Projects = projectRepo.getAll();
                        SelectedProject = project;
                    }));
            }
        }

        /// <summary>
        /// Gets the activity enum.
        /// </summary>
        /// <value>
        /// The activity enum.
        /// </value>
        public IEnumerable<Activity> ActivityEnum
        {
            get
            {
                return Enum.GetValues(typeof(Activity)).Cast<Activity>();
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
        /// Gets or sets the selected admin.
        /// </summary>
        /// <value>
        /// The selected admin.
        /// </value>
        public Employee SelectedAdmin
        {
            get { return selectedAdmin; }
            set
            {
                selectedAdmin = value;
                OnPropertyChanged("SelectedAdmin");
            }
        }

        /// <summary>
        /// Gets or sets the selected project.
        /// </summary>
        /// <value>
        /// The selected project.
        /// </value>
        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        /// <summary>
        /// Gets or sets the selected task.
        /// </summary>
        /// <value>
        /// The selected task.
        /// </value>
        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlPanelViewModel"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="empl">The current empl.</param>
        public ControlPanelViewModel(DBContext context, Employee empl)
        {
            dbContext = context;
            employeeRepo = new EmployeeImpl(dbContext);
            //adminRepo = new AdminImpl(dbContext);
            projectRepo = new ProjectImpl(dbContext);
            reportRepo = new ReportImpl(dbContext);
            taskRepo = new TaskImpl(dbContext);

            Employees = new ObservableCollection<Employee>(employeeRepo.getAll().Where(user => user.Role == Role.User));
            Admins = new ObservableCollection<Employee>(employeeRepo.getAll().Where(user => user.Role == Role.Admin));
            Projects = projectRepo.getAll();
            Tasks = taskRepo.getAll();

            EmployeeSelectedIndex = -1;
            AdminSelectedIndex = -1;
            ProjectSelectedIndex = -1;
            TaskSelectedIndex = -1;

            SelectedProject = new Project();
            SelectedTask = new Task();
            SelectedEmployee = new Employee();
            SelectedAdmin = new Employee();
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
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
                if (employeeRepo != null)
                {
                    employeeRepo.Dispose();
                    employeeRepo = null;
                }
                if (projectRepo != null)
                {
                    projectRepo.Dispose();
                    projectRepo = null;
                }
                if (reportRepo != null)
                {
                    reportRepo.Dispose();
                    reportRepo = null;
                }
                if (taskRepo != null)
                {
                    taskRepo.Dispose();
                    taskRepo = null;
                }
            }
        }
    }
}
