using ControlzEx.Standard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> about = new List<User>();
        User_Controller user_Controller = new User_Controller();

        int number = 0;
        int number2 = 0;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (number2 == 0)
            {

                       if (usernameTxb.Text!=""&&passwordTxb.Text!=""&&nameTxb.Text!="")
                       {
                       
                       User user1 = new User(usernameTxb.Text, passwordTxb.Text, nameTxb.Text);
                       about.Add(user1);
                       
                       var serializer = new JsonSerializer();
                       using (var sw = new StreamWriter("Informations.json"))
                       {
                           using (var jw = new JsonTextWriter(sw))
                           {
                               jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                               serializer.Serialize(jw, about[0].Alltext);
                           }
                       }
                           //SecondPage();
                               number2 = 1;
                           }
                       else
                       {
                           MessageBox.Show("Fill in the blanks");
                       }
            }
           if (number2 == 1)
            {
                user_Controller.Add(about[0]);

                number = 1;
                MainLbl.Content = "Sign In";
                usernameTxb.Text = "";
                passwordTxb.Text = "";
                nameTxb.Text = "";
                grid.Children.Remove(nameLbl);
                grid.Children.Remove(nameTxb);
                //grid.Children.Remove(nextBtn);

                //Button button2 = new Button();
                //button2.Margin = new Thickness(561, 340, 143, 31);
                //button2.Content = "Next";
                //button2.FontSize = 26;
                //button2.Background = Brushes.Aqua;
                //button2.Height = 40;
                //button2.Width = 70;

                //grid.Children.Add(button2);

                number2 = 2;


            }
            else if (number2 == 2)
                {
                if (user_Controller.LoginTime.Minute < DateTime.Now.Minute)
                {
                    if(user_Controller.LoginTime.Minute+1 == DateTime.Now.Minute && user_Controller.LoginTime.Second <= DateTime.Now.Second)
                    {
                        //MessageBox.Show("a");
                        File.Delete($"Informations.json");
                        about.RemoveAt(0);
                        Delete();
                    }
                    else if(user_Controller.LoginTime.Minute + 1 < DateTime.Now.Minute)
                    {
                        //MessageBox.Show("a");
                        File.Delete($"Informations.json");
                        about.RemoveAt(0);
                        Delete();
                    }
                }
                else
                {

                Order();
                }
             
                }
        }
        
        public void Order()
        {
            grid.Children.Remove(usernameTxb);
            grid.Children.Remove(passwordTxb);
            grid.Children.Remove(usernameLbl);
            grid.Children.Remove(passwordLbl);
            MainLbl.Content = "Order Now";

        }

        public void Delete()
        {
            grid.Children.Remove(usernameTxb);
            grid.Children.Remove(passwordTxb);
            grid.Children.Remove(usernameLbl);
            grid.Children.Remove(passwordLbl);
            MainLbl.Content = "not Account";
        }

        
        public void SecondPage()
        {
           
            user_Controller.Add(about[0]);

            number = 1;
            MainLbl.Content = "Sign In";
            usernameTxb.Text = "";
            passwordTxb.Text = "";
            nameTxb.Text = "";
            grid.Children.Remove(nameLbl);
            grid.Children.Remove(nameTxb);
            grid.Children.Remove(nextBtn);

            //Button button2 = new Button();
            //button2.Margin = new Thickness(561, 340, 143, 31);
            //button2.Content = "Next";
            //button2.FontSize = 26;
            //button2.Background = Brushes.Aqua;
            //button2.Height = 40;
            //button2.Width = 70;
            
            //grid.Children.Add(button2);

            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            //Thread.Sleep(60000);
            //stopWatch.Stop();
            //// Get the elapsed time as a TimeSpan value.
            //TimeSpan ts = stopWatch.Elapsed;

            //// Format and display the TimeSpan value.
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime);





           


        }

        class User_Controller
        {
            public DateTime LoginTime { get; set; }
            public string User_nick { get; set; }
            public User_Controller()
            {

            }
            public User_Controller(User user)
            {
                LoginTime = DateTime.Now;
                User_nick = user.Username;
            }
            public void Add(User user)
            {
                LoginTime = DateTime.Now;
                User_nick = user.Username;
            }

        }

        class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Alltext { get; set; }
            public DateTime CreationDate { get; set; } = DateTime.Now;

            public User()
            {

            }

            public User(string username, string password, string name)
            {
                Username = username;
                Password = password;
                Name = name;
                Alltext = Username + " - " + Password + " - " + Name;
            }

            
        }
    }
}
