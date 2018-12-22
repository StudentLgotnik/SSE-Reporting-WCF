using ServiceReference1;
using SSE_Reporting.Dao;
using SSE_Reporting.Dao.Impl;
using SSE_Reporting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Windows;

namespace SSE_Reporting.ViewModel
{
    class ChatViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The client
        /// </summary>
        MyServiceClient client = null;
        /// <summary>
        /// The database context
        /// </summary>
        private DBContext dbContext;
        /// <summary>
        /// The selected employee
        /// </summary>
        private Employee selectedEmployee;
        /// <summary>
        /// The current employee
        /// </summary>
        private Employee currentEmployee;
        /// <summary>
        /// The employee selected index
        /// </summary>
        private int employeeSelectedIndex;
        /// <summary>
        /// The selected message
        /// </summary>
        private Message selectedMessage;
        /// <summary>
        /// The ritch text
        /// </summary>
        private string ritchText;

        /// <summary>
        /// The employees
        /// </summary>
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        /// <summary>
        /// The employee repo
        /// </summary>
        IRepository<Employee> employeeRepo;
        /// <summary>
        /// The message repo
        /// </summary>
        IRepository<Message> messageRepo;
        /// <summary>
        /// The sent message
        /// </summary>
        private RelayCommand sentMessage;

        /// <summary>
        /// Gets or sets the employees
        /// </summary>
        /// <value>
        /// The employees
        /// </value>
        public ObservableCollection<Employee> Empls
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
}
        }

        /// <summary>
        /// Gets or sets the index of the employee selected.
        /// </summary>
        /// <value>
        /// The index of the employee selected.
        /// </value>
        public int EmplSelectedIndex
        {
            get { return employeeSelectedIndex; }
            set { employeeSelectedIndex = value; OnPropertyChanged("EmplSelectedIndex"); }
        }

        /// <summary>
        /// Gets or sets the selected message.
        /// </summary>
        /// <value>
        /// The selected message.
        /// </value>
        public Message SelectedMessage
        {
            get { return selectedMessage; }
            set
            {
                selectedMessage = value;
                OnPropertyChanged("SelctedMessage");
            }
        }

        /// <summary>
        /// Gets or sets the ritch text.
        /// </summary>
        /// <value>
        /// The ritch text.
        /// </value>
        public string RitchText
        {
            get { return ritchText; }
            set
            {
                ritchText = value;
                OnPropertyChanged("RitchText");
            }
        }

        /// <summary>
        /// Gets or sets the selected employee.
        /// </summary>
        /// <value>
        /// The selected employee.
        /// </value>
        public Employee SelectedEmpl
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        /// <summary>
        /// Gets the sent message.
        /// </summary>
        /// <value>
        /// The sent message.
        /// </value>
        public RelayCommand SentMessage
        {
            get
            {
                return sentMessage ??
                    (sentMessage = new RelayCommand(obj =>
                    {
                        if (SelectedEmpl.Id != 0)
                        {
                            string mess = client.SendEmplMessage(currentEmployee.Login, SelectedMessage.Messagee, SelectedEmpl.Login);
                            Print(mess + "\n");
                            SelectedMessage.Messagee = mess;
                            messageRepo.save(SelectedMessage);
                            SelectedEmpl = new Employee();
                            SelectedMessage.Messagee = "";
                        }
                        else if(SelectedEmpl.Id == 0)
                        {
                            string mess = client.SendMessage(currentEmployee.Login, SelectedMessage.Messagee);
                            Print(mess + "\n");
                            SelectedMessage.Messagee = mess;
                            messageRepo.save(SelectedMessage);
                            SelectedEmpl = new Employee();
                            SelectedMessage.Messagee = "";
                        }
                    }
                    ));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatViewModel"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="empl">The empl.</param>
        public ChatViewModel(DBContext context, Employee empl)
        {
            dbContext = context;
            employeeRepo = new EmployeeImpl(dbContext);
            messageRepo = new MessageImpl(dbContext);
            currentEmployee = empl;
            Create_New_Client();

            Empls = new ObservableCollection<Employee>(employeeRepo.getAll().Where(user => user.Id != empl.Id));
            setRichText();
            EmplSelectedIndex = -1;
            SelectedEmpl = new Employee();
            SelectedMessage = new Message(empl);
        }

        /// <summary>
        /// Creates the new client.
        /// </summary>
        private void Create_New_Client()
        {
            if (client == null)
                try { Try_To_Create_New_Client(); }
                catch (Exception ex)
                {
                    Print(ex);
                    Print(ex.InnerException);
                    client = null;
                }
            else
            {
                Print("Cannot create a new client. The current Client is active.");
            }
        }

        /// <summary>
        /// Tries to create new client.
        /// </summary>
        private void Try_To_Create_New_Client()
        {
            try
            {

                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);

                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

                string uri = "net.tcp://192.168.128.103:9002/MyService";

                EndpointAddress endpoint = new EndpointAddress(new Uri(uri));

                client = new MyServiceClient(binding, endpoint);

            }
            catch (Exception ex)
            {
                Print(ex);
                Print(ex.InnerException);
                client = null;
            }
        }

        /// <summary>
        /// Sets the rich text.
        /// </summary>
        private void setRichText()
        {
            foreach(Message m in messageRepo.getAll())
            {
                Print(m.Messagee + "\n");
            }
        }

        /// <summary>
        /// Prints the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        private void Print(string text)
        {
            RitchText += text;
        }

        /// <summary>
        /// Prints the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void Print(Exception ex)
        {
            if (ex == null) return;
            Print(ex.Message);
            Print(ex.Source);
            Print(ex.StackTrace);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
