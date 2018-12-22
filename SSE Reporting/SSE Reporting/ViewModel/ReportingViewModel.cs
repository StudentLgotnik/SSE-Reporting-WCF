using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using SSE_Reporting.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace SSE_Reporting.ViewModel
{
    class ReportingViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext dbContext;

        /// <summary>
        /// The employee repo
        /// </summary>
        IRepository<Employee> employeeRepo;
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
        /// The current empl
        /// </summary>
        private Employee currentEmpl;
        /// <summary>
        /// The selected day
        /// </summary>
        private DateTime selectedDay;
        /// <summary>
        /// The selected month
        /// </summary>
        private string selectedMonth;
        /// <summary>
        /// The selected report
        /// </summary>
        private Report selectedReport;

        /// <summary>
        /// The monday header
        /// </summary>
        private string mondayHeader;
        /// <summary>
        /// The tuesday header
        /// </summary>
        private string tuesdayHeader;
        /// <summary>
        /// The wednesday header
        /// </summary>
        private string wednesdayHeader;
        /// <summary>
        /// The thursday header
        /// </summary>
        private string thursdayHeader;
        /// <summary>
        /// The friday header
        /// </summary>
        private string fridayHeader;
        /// <summary>
        /// The saturday header
        /// </summary>
        private string saturdayHeader;
        /// <summary>
        /// The sunday header
        /// </summary>
        private string sundayHeader;

        /// <summary>
        /// The selected index
        /// </summary>
        private int selectedIndex;
        /// <summary>
        /// The report command
        /// </summary>
        private string reportCommand;

        /// <summary>
        /// The add report
        /// </summary>
        private RelayCommand addReport;
        /// <summary>
        /// The delete report
        /// </summary>
        private RelayCommand deleteReport;
        /// <summary>
        /// The next week
        /// </summary>
        private RelayCommand nextWeek;
        /// <summary>
        /// The previous week
        /// </summary>
        private RelayCommand previousWeek;
        /// <summary>
        /// The information
        /// </summary>
        private RelayCommand info;
        /// <summary>
        /// The chat
        /// </summary>
        private RelayCommand chat;
        /// <summary>
        /// The admin panel
        /// </summary>
        private RelayCommand adminPanel;

        /// <summary>
        /// The monday reports
        /// </summary>
        private ObservableCollection<Report> mondayReports = new ObservableCollection<Report>();
        /// <summary>
        /// The tuesday reports
        /// </summary>
        private ObservableCollection<Report> tuesdayReports = new ObservableCollection<Report>();
        /// <summary>
        /// The wednwsday reports
        /// </summary>
        private ObservableCollection<Report> wednwsdayReports = new ObservableCollection<Report>();
        /// <summary>
        /// The thursday reports
        /// </summary>
        private ObservableCollection<Report> thursdayReports = new ObservableCollection<Report>();
        /// <summary>
        /// The friday reports
        /// </summary>
        private ObservableCollection<Report> fridayReports = new ObservableCollection<Report>();
        /// <summary>
        /// The saturday reports
        /// </summary>
        private ObservableCollection<Report> saturdayReports = new ObservableCollection<Report>();
        /// <summary>
        /// The sunday reports
        /// </summary>
        private ObservableCollection<Report> sundayReports = new ObservableCollection<Report>();

        /// <summary>
        /// The monday progress
        /// </summary>
        private double mondayProgress;
        /// <summary>
        /// The tuesday progress
        /// </summary>
        private double tuesdayProgress;
        /// <summary>
        /// The wednesday progress
        /// </summary>
        private double wednesdayProgress;
        /// <summary>
        /// The thursday progress
        /// </summary>
        private double thursdayProgress;
        /// <summary>
        /// The friday progress
        /// </summary>
        private double fridayProgress;
        /// <summary>
        /// The saturday progress
        /// </summary>
        private double saturdayProgress;
        /// <summary>
        /// The sunday progress
        /// </summary>
        private double sundayProgress;

        /// <summary>
        /// The projects
        /// </summary>
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        /// <summary>
        /// The tasks
        /// </summary>
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        //private ObservableCollection<Activity> activityEnum = new ObservableCollection<Activity>();

        /// <summary>
        /// Property of selected report
        /// </summary>
        public Report SelectedReport
        {
            get { return selectedReport; }
            set
            {
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
            }
        }

        /// <summary>
        /// Property of report command
        /// </summary>
        public string ReportCommand
        {
            get
            {
                return reportCommand;
            }
            set
            {
                reportCommand = value;
                OnPropertyChanged("ReportCommand");
            }
        }

        /// <summary>
        /// Index of selected TabItem
        /// </summary>
        public int SelectedTabIndex {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (getWeekIndex(selectedDay) < value)
                {
                    selectedDay = selectedDay.AddDays(value - getWeekIndex(selectedDay));
                }
                else
                {
                    selectedDay = selectedDay.AddDays(-(getWeekIndex(selectedDay) - value));
                }
                SelectedReport = new Report();
                selectedIndex = value;
            }
        }

        /// <summary>
        /// List of Monday reports
        /// </summary>
        public ObservableCollection<Report> MondayReports
        {
            get { return mondayReports; }
            set {
                mondayReports = value;
                OnPropertyChanged("MondayReports");
            }
        }

        /// <summary>
        /// List of Tuesday reports
        /// </summary>
        public ObservableCollection<Report> TuesdayReports
        {
            get { return tuesdayReports; }
            set {
                tuesdayReports = value;
                OnPropertyChanged("TuesdayReports");
            }
        }

        /// <summary>
        /// List of Wednesday reports
        /// </summary>
        public ObservableCollection<Report> WednesdayReports
        {
            get { return wednwsdayReports; }
            set {
                wednwsdayReports = value;
                OnPropertyChanged("WednesdayReports");
            }
        }

        /// <summary>
        /// List of Thursday reports
        /// </summary>
        public ObservableCollection<Report> ThursdayReports
        {
            get { return thursdayReports; }
            set {
                thursdayReports = value;
                OnPropertyChanged("ThursdayReports");
            }
        }

        /// <summary>
        /// List of Friday reports
        /// </summary>
        public ObservableCollection<Report> FridayReports
        {
            get { return fridayReports; }
            set {
                fridayReports = value;
                OnPropertyChanged("FridayReports");
            }
        }

        /// <summary>
        /// List of Saturday reports
        /// </summary>
        public ObservableCollection<Report> SaturdayReports
        {
            get { return saturdayReports; }
            set {
                saturdayReports = value;
                OnPropertyChanged("SaturdayReports");
            }
        }

        /// <summary>
        /// List of Sunday reports
        /// </summary>
        public ObservableCollection<Report> SundayReports
        {
            get { return sundayReports; }
            set {
                sundayReports = value;
                OnPropertyChanged("SundayReports");
            }
        }

        /// <summary>
        /// List of projacts
        /// </summary>
        public ObservableCollection<Project> Projects 
        {
            get { return projects; }
            set { projects = value; }
        }

        /// <summary>
        /// List of tasks
        /// </summary>
        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        /// <summary>
        /// Property of month that is selected 
        /// </summary>
        public string SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                OnPropertyChanged("SelectedMonth");
            }
        }

        /// <summary>
        /// Property of TubIem header for Monday 
        /// </summary>
        public string MondayHeader
        {
            get { return mondayHeader; }
            set
            {
                mondayHeader = value;
                OnPropertyChanged("MondayHeader");
            }
        }

        /// <summary>
        /// Property of TubIem header for Tuesday 
        /// </summary>
        public string TuesdayHeader
        {
            get { return tuesdayHeader; }
            set
            {
                tuesdayHeader = value;
                OnPropertyChanged("TuesdayHeader");
            }
        }

        /// <summary>
        /// Property of TubIem header for Wednesday 
        /// </summary>
        public string WednesdayHeader
        {
            get { return wednesdayHeader; }
            set
            {
                wednesdayHeader = value;
                OnPropertyChanged("WednesdayHeader");
            }
        }

        /// <summary>
        /// Property of TubIem header for Thursady 
        /// </summary>
        public string ThursdayHeader
        {
            get { return thursdayHeader; }
            set
            {
                thursdayHeader = value;
                OnPropertyChanged("ThursdayHeader");
            }
        }

        /// <summary>
        /// Property of TubIem header for Friday 
        /// </summary>
        public string FridayHeader
        {
            get { return fridayHeader; }
            set
            {
                fridayHeader = value;
                OnPropertyChanged("FridayHeader");
            }
        }

        /// <summary>
        /// Property of TubIem header for Saturday 
        /// </summary>
        public string SaturdayHeader
        {
            get { return saturdayHeader; }
            set
            {
                saturdayHeader = value;
                OnPropertyChanged("SaturdayHeader");
            }
        }

        /// <summary>
        /// Property of TubIem header for Sunday 
        /// </summary>
        public string SundayHeader
        {
            get { return sundayHeader; }
            set
            {
                sundayHeader = value;
                OnPropertyChanged("SundayHeader");
            }
        }


        /// <summary>
        /// Property of Monday progress
        /// </summary>
        public double MondayProgress
        {
            get { return mondayProgress; }
            set
            {
                mondayProgress = value;
                OnPropertyChanged("MondayProgress");
            }
        }


        /// <summary>
        /// Property of Tuesday progress
        /// </summary>
        public double TuesdayProgress
        {
            get { return tuesdayProgress; }
            set
            {
                tuesdayProgress = value;
                OnPropertyChanged("TuesdayProgress");
            }
        }


        /// <summary>
        /// Property of Wednesday progress
        /// </summary>
        public double WednesdayProgress
        {
            get { return wednesdayProgress; }
            set
            {
                wednesdayProgress = value;
                OnPropertyChanged("WednesdayProgress");
            }
        }


        /// <summary>
        /// Property of Thursday progress
        /// </summary>
        public double ThursdayProgress
        {
            get { return thursdayProgress; }
            set
            {
                thursdayProgress = value;
                OnPropertyChanged("ThursdayProgress");
            }
        }


        /// <summary>
        /// Property of Friday progress
        /// </summary>
        public double FridayProgress
        {
            get { return fridayProgress; }
            set
            {
                fridayProgress = value;
                OnPropertyChanged("FridayProgress");
            }
        }


        /// <summary>
        /// Property of Saturday progress
        /// </summary>
        public double SaturdayProgress
        {
            get { return saturdayProgress; }
            set
            {
                saturdayProgress = value;
                OnPropertyChanged("SaturdayProgress");
            }
        }

        /// <summary>
        /// Property of Sunday progress
        /// </summary>
        public double SundayProgress
        {
            get { return sundayProgress; }
            set
            {
                sundayProgress = value;
                OnPropertyChanged("SundayProgress");
            }
        }


        /// <summary>
        /// Enum of activity
        /// </summary>
        public IEnumerable<Activity> ActivityEnum
        {
            get
            {
                return Enum.GetValues(typeof(Activity)).Cast<Activity>();
            }
        }

        /// <summary>
        /// Command to update or add report
        /// </summary>
        public RelayCommand AddReport
        {
            get
            {
                return addReport ??
                    (addReport = new RelayCommand(obj =>
                    {
                        if (SelectedReport.Equals(reportRepo.get(SelectedReport.Id)))
                        {
                            reportRepo.update(SelectedReport);
                            SelectedReport = new Report();
                        }
                        else if (validTimeCheck(SelectedReport.StartHours, SelectedReport.EndHours))
                        {
                            if (SelectedReport.Activity == Activity.TimeOff)
                            {
                                if ((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours > (double)8)
                                {
                                    currentEmpl.TimeOff = currentEmpl.TimeOff - 1;
                                }
                                else
                                {
                                    currentEmpl.TimeOff = currentEmpl.TimeOff - (double)((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours) / 8;
                                }
                            }
                            else if (SelectedReport.Activity == Activity.Sickness)
                            {
                                if ((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours > (double)8)
                                {
                                    currentEmpl.Sickness = currentEmpl.Sickness - 1;
                                }
                                else
                                {
                                    currentEmpl.Sickness = currentEmpl.Sickness - (double)((SelectedReport.EndHours - SelectedReport.StartHours).TotalHours) / 8;
                                }
                            }
                            else
                            {
                                currentEmpl.TimeOff = currentEmpl.TimeOff + timeOffFromReport(SelectedReport.StartHours, selectedReport.EndHours);
                            }
                            
                            Report report = reportRepo.save(new Report
                            {
                                Project = SelectedReport.Project,
                                Task = SelectedReport.Task,
                                Activity = SelectedReport.Activity,
                                EmployeeId = currentEmpl.Id,
                                StartHours = SelectedReport.StartHours,
                                EndHours = SelectedReport.EndHours,
                                Date = selectedDay
                            });
                            addToCollection(getWeekIndex(selectedDay), report);
                            SelectedReport = new Report();
                            
                        }
                        employeeRepo.update(currentEmpl);
                        setWeekProgress();
                    }));
            }
        }

        /// <summary>
        /// Command to delete report
        /// </summary>
        public RelayCommand DeleteReport
        {
            get
            {
                return deleteReport ??
                    (deleteReport = new RelayCommand(obj =>
                    {
                        if (!(SelectedReport.Id == 0))
                        {
                            Report report = reportRepo.delete(SelectedReport);
                            getDayCollection(SelectedTabIndex).Remove(SelectedReport);
                            SelectedReport = new Report();
                        }
                        currentEmpl.TimeOff = currentEmpl.TimeOff - timeOffFromReport(selectedReport.StartHours, selectedReport.EndHours);
                        employeeRepo.update(currentEmpl);
                        setWeekProgress();
                    }
                    ));
            }
        }

        /// <summary>
        /// Command to open next week
        /// </summary>
        public RelayCommand NextWeek
        {
            get
            {
                return nextWeek ??
                    (nextWeek = new RelayCommand(obj =>
                    {
 
                        selectedDay =  selectedDay.AddDays(7);
                        SelectedTabIndex = getWeekIndex(selectedDay);
                        setWeekCollection(selectedDay);
                        setWeekRange(selectedDay);
                    }
                    ));
            }
        }

        /// <summary>
        /// Command to open previous week
        /// </summary>
        public RelayCommand PreviousWeek
        {
            get
            {
                return previousWeek ??
                    (previousWeek = new RelayCommand(obj =>
                    {
                        selectedDay = selectedDay.AddDays(-7);
                        SelectedTabIndex = getWeekIndex(selectedDay);
                        setWeekCollection(selectedDay);
                        setWeekRange(selectedDay);
                    }
                    ));
            }
        }

        /// <summary>
        /// Command to open user info
        /// </summary>
        public RelayCommand Info
        {
            get
            {
                return info ??
                    (info = new RelayCommand(obj =>
                    {
                        Employee_Info ei = new Employee_Info(dbContext, currentEmpl);
                        ei.ShowDialog();
                    }
                    ));
            }
        }

        /// <summary>
        /// Command to open chat
        /// </summary>
        public RelayCommand Chat
        {
            get
            {
                return chat ??
                    (chat = new RelayCommand(obj =>
                    {
                        Chat ei = new Chat(dbContext, currentEmpl);
                        ei.ShowDialog();
                    }
                    ));
            }
        }

        /// <summary>
        /// Command to open admin panel
        /// </summary>
        public RelayCommand AdminPanel
        {
            get
            {
                return adminPanel ??
                    (adminPanel = new RelayCommand(obj =>
                    {
                        ControlPanel cp = new ControlPanel(dbContext, currentEmpl);
                        cp.ShowDialog();
                    }
                    ));
            }
        }

        /// <summary>
        /// Cunstructor
        /// </summary>
        /// <param name="context">Context of database</param>
        /// <param name="empl">Logined user</param>
        public ReportingViewModel(DBContext context, Employee empl)
        {
            dbContext = context;
            employeeRepo = new EmployeeImpl(dbContext);
            projectRepo = new ProjectImpl(dbContext);
            reportRepo = new ReportImpl(dbContext);
            taskRepo = new TaskImpl(dbContext);
            currentEmpl = empl;
            selectedDay = DateTime.Now.Date;
            setWeekCollection(selectedDay);
            setWeekRange(selectedDay);
            SelectedTabIndex = getWeekIndex(selectedDay);
            SelectedReport = new Report();

            if (empl.ProjectId != null)
            {
                Project pr = projectRepo.get((int)empl.ProjectId);
                Projects.Add(pr);
                Tasks = new ObservableCollection<Task>(taskRepo.getAll().Where(task => task.ProjectId == pr.Id));
            }

        }

        /// <summary>
        /// Reset collections according to entered day
        /// </summary>
        /// <param name="date">Selected weekday</param>
        private void setWeekCollection(DateTime date)
        {
            MondayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay)).Date && report.EmployeeId == currentEmpl.Id));
            TuesdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 1).Date && report.EmployeeId == currentEmpl.Id));
            WednesdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 2).Date && report.EmployeeId == currentEmpl.Id));
            ThursdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 3).Date && report.EmployeeId == currentEmpl.Id));
            FridayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 4).Date && report.EmployeeId == currentEmpl.Id));
            SaturdayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 5).Date && report.EmployeeId == currentEmpl.Id));
            SundayReports = new ObservableCollection<Report>(reportRepo.getAll().Where(report => report.Date.Date == selectedDay.AddDays(-getWeekIndex(selectedDay) + 6).Date && report.EmployeeId == currentEmpl.Id));

        }

        /// <summary>
        /// Reset label values according to entered day
        /// </summary>
        /// <param name="day">Selected weekday</param>
        private void setWeekRange(DateTime day)
        {
            int dayOfWeek = getWeekIndex(day);
            DateTime MondayDay = day.AddDays(-dayOfWeek);
            DateTime SundayDay = day.AddDays(+6 - dayOfWeek);
            if (day.AddDays(-dayOfWeek).Month == day.AddDays(+6 - dayOfWeek).Month)
            {
                SelectedMonth = string.Format("{0} {1} - {2}, {3}",
                    day.ToString("MMMM", CultureInfo.InvariantCulture),
                    MondayDay.Day,
                    SundayDay.Day,
                    day.Year);
            }
            else
            {
                SelectedMonth = string.Format("{0} {1} - {2} {3}, {4}",
                    day.AddDays(-dayOfWeek).ToString("MMMM", CultureInfo.InvariantCulture),
                    MondayDay.Day,
                    day.AddDays(+6 - dayOfWeek).ToString("MMMM", CultureInfo.InvariantCulture),
                    SundayDay.Day,
                    day.Year);
            }
            MondayHeader = string.Format("{0} {1}", day.AddDays( - dayOfWeek).Day, "Monday");
            TuesdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +1).Day, "Tuesday");
            WednesdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +2).Day, "Wednesday");
            ThursdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +3).Day, "Thursday");
            FridayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +4).Day, "Friday");
            SaturdayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +5).Day, "Saturday");
            SundayHeader = string.Format("{0} {1}", day.AddDays(- dayOfWeek +6).Day, "Sunday");
            setWeekProgress();
        }

        /// <summary>
        /// Reset value of progress bar
        /// </summary>
        private void setWeekProgress()
        {
            MondayProgress = getWorkedTime(MondayReports);
            TuesdayProgress = getWorkedTime(TuesdayReports);
            WednesdayProgress = getWorkedTime(WednesdayReports);
            ThursdayProgress = getWorkedTime(ThursdayReports);
            FridayProgress = getWorkedTime(FridayReports);
            SaturdayProgress = getWorkedTime(SaturdayReports);
            SundayProgress = getWorkedTime(SundayReports);
        }

        /// <summary>
        /// Get double value of vorked time of collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>double value of worked time of day</returns>
        private double getWorkedTime(ObservableCollection<Report> collection)
        {
            double result = 0.0;
            foreach (Report report in collection)
            {
                result = result + (report.EndHours - report.StartHours).TotalHours;
            }
            return result;
        }

        /// <summary>
        /// Get weekday index
        /// </summary>
        /// <param name="date">Day for getting index</param>
        /// <returns>Index of weekday</returns>
        private int getWeekIndex(DateTime date)
        {
            return (int)(date.DayOfWeek + 6) % 7;
        }

        /// <summary>
        /// Count avaliable time off from reported time
        /// </summary>
        /// <param name="Start">Stsrt reported time</param>
        /// <param name="End">End reported time</param>
        /// <returns>Double time off value of reported time</returns>
        private double timeOffFromReport(TimeSpan Start, TimeSpan End)
        {
            //List<Report> reports = new List<Report>();
            //reports.AddRange(reportRepo.getAll().Where(report => report.EmployeeId == currentEmpl.Id));
            //TimeSpan result = new TimeSpan();
            //foreach (Report report in reports)
            //{
            //    result += report.EndHours - report.StartHours;
            //}
            double timeOffInHour = (double)18 / 2016;
            double workedHours = (End-Start).TotalHours;
            return Math.Round(workedHours * timeOffInHour, 4);
        }

        /// <summary>
        /// Validates entered TimeSpan
        /// </summary>
        /// <param name="startTS">Start time</param>
        /// <param name="endTS">End time</param>
        /// <returns>Is valid period or not</returns>
        private bool validTimeCheck(TimeSpan startTS, TimeSpan endTS)
        {
            if (TimeSpan.Compare(startTS, endTS) == 1)
            {
                MessageBox.Show("Start time is greater than end time!");
                return false;
            }
            if (TimeSpan.Compare(startTS, endTS) == 0)
            {
                MessageBox.Show("Start and end times are the same!");
                return false;
            }

            foreach (Report report in getDayCollection(SelectedTabIndex))
            {
                if (betweenTS(startTS, report.StartHours, report.EndHours))
                {
                    MessageBox.Show("Selected time is already reported!");
                    return false;
                }
                if (betweenTS(endTS, report.StartHours, report.EndHours))
                {
                    MessageBox.Show("Selected time is already reported!");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Set Report to Collection by index
        /// </summary>
        /// <param name="index">Weekday index</param>
        /// <param name="report">Report for adding</param>
        private void addToCollection(int index, Report report)
        {
            switch (index)
            {
                case 0:
                    MondayReports.Add(report);
                    break;
                case 1:
                    TuesdayReports.Add(report);
                    break;
                case 2:
                    WednesdayReports.Add(report);
                    break;
                case 3:
                    ThursdayReports.Add(report);
                    break;
                case 4:
                    FridayReports.Add(report);
                    break;
                case 5:
                    SaturdayReports.Add(report);
                    break;
                case 6:
                    SundayReports.Add(report);
                    break;
            }
        }

        /// <summary>
        /// Get nedded day collection
        /// </summary>
        /// <param name="index">Weekday index</param>
        /// <returns>Collection of week day by index</returns>
        private ObservableCollection<Report> getDayCollection(int index)
        {
            switch (index)
            {
                case 0: return MondayReports;
                case 1: return TuesdayReports;
                case 2: return WednesdayReports;
                case 3: return ThursdayReports;
                case 4: return FridayReports;
                case 5: return SaturdayReports;
                case 6: return SundayReports;
            }
            return null;
        }


        /// <summary>
        /// Checks entry ts to period between top and bot
        /// </summary>
        /// <param name="ts"> TimeSpan for checking</param>
        /// <param name="top">Top limit of TimeSpan</param>
        /// <param name="bot">Bot limit of TimeSpan</param>
        /// <returns>true if perid contains ts, else return false</returns>
        private bool betweenTS(TimeSpan ts, TimeSpan top, TimeSpan bot)
        {
            if ((TimeSpan.Compare(ts, bot) == 1 && TimeSpan.Compare(ts, top) == -1) || (TimeSpan.Compare(ts, bot) == -1 && TimeSpan.Compare(ts, top) == 1))
            {
                return true;
            }
            return false;
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

        public Action Close { get; set; }
    }
}
