using ServiceReference1;
using SSE_Reporting.Model;
using SSE_Reporting.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SSE_Reporting.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Chat : Window
    {

        MyServiceClient client = null;


        public Chat(DBContext dbContext, Employee currentEmpl)
        {
            InitializeComponent();
            DataContext = new ChatViewModel(dbContext, currentEmpl);
        }


        private void Print(string text)
        {
            richTextBox1.Document.Blocks.Add(new Paragraph(new Run(text + "\n\n")));
            //richTextBox1. = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            richTextBox1.ScrollToEnd();
        }

        private void Print(Exception ex)
        {
            if (ex == null) return;
            Print(ex.Message);
            Print(ex.Source);
            Print(ex.StackTrace);
        }

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

        private void Try_To_Create_New_Client()
        {
            try
            {

                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);

                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

                string uri = "net.tcp://10.4.139.75:9002/MyService";

                EndpointAddress endpoint = new EndpointAddress(new Uri(uri));

                client = new MyServiceClient(binding, endpoint);


                //client.ClientCredentials.Windows.ClientCredential.Domain = "";
                //client.ClientCredentials.Windows.ClientCredential.UserName = "Student";
                //client.ClientCredentials.Windows.ClientCredential.Password = "12345";

                Print("Creating new client ....");
                Print(endpoint.Uri.ToString());
                Print(uri);

                string test = client.SendMessage("Maxim", "test");

                if (test.Length < 1)
                {
                    throw new Exception("Проверка соединения не удалась");
                }
                else
                {
                    Print("test is OK  ! " + test);
                }

            }
            catch (Exception ex)
            {
                Print(ex);
                Print(ex.InnerException);
                client = null;
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Create_New_Client();
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Print("sending message . . .");
            string s = textBox1.Text;
            string x = "";
            if (client != null)
            {
                x = client.SendMessage("Maxim", s);
                Print(x);
                x = client.SendMessage("Maxim", s);
                Print(x);
            }
            else
            {
                Print("Error! Client does not exist!");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                Print("Closing a client ...");
                client.Close();
                client = null;
            }
            else
            {
                Print("Error! Client does not exist!");
            }
            this.Close();
        }

    }
}

