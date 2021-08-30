using AppChef.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;

namespace AppChef
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        static Regex validate_emailaddress = email_validation();

        static Regex rgx = new Regex("[^A-Za-z0-9]");

        public List<Users> u = new List<Users>();
        public Register()
        {
            InitializeComponent();
        }
        private static Regex email_validation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public Boolean upperCase(string str)
        {
            Boolean validate = false;
            foreach (char c in str)
                if (char.IsUpper(c))
                    validate = true;
            return validate;
        }

        public Boolean length(string str)
        {
            Boolean validate = false;
            if (str.Length >= 7)
                validate = true;
            return validate;
        }

        public Boolean isDigit(string str)
        {
            Boolean validate = false;
            foreach (char c in str)
                if (char.IsDigit(c))
                    validate = true;
            return validate;
        }

        public Boolean specialCharacter(string str)
        {
            Boolean validate = rgx.IsMatch(str);
            return validate;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int valide = 0;
            if (usernameTextBox.Text.Length > 0)
                if (validate_emailaddress.IsMatch(emailTextBox.Text))
                {
                    Boolean validateUpperCase, validateSpecialCharacter, validateLength, validateIsDigit;
                    validateUpperCase = upperCase(passwordTextBox.Password.ToString());
                    validateSpecialCharacter = specialCharacter(passwordTextBox.Password.ToString());
                    validateLength = length(passwordTextBox.Password.ToString());
                    validateIsDigit = isDigit(passwordTextBox.Password.ToString());
                    if (validateUpperCase && validateIsDigit && validateLength && validateSpecialCharacter)
                        if (passwordConfirmationTextBox.Password.ToString() == passwordTextBox.Password.ToString())
                            valide = 1;
                        else
                            MessageBox.Show("Passwords do not match");
                    else
                        MessageBox.Show("Password format is not good");

                }
                else
                    MessageBox.Show("Email is not valid");
            else
                MessageBox.Show("Enter a username");

            SqlConnection con = new SqlConnection(@"Data Source =DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            SqlCommand cmd2 = con.CreateCommand();
           // cmd2.CommandText = "SET IDENTITY_INSERT Users ON";
           // cmd2.ExecuteNonQuery();

            SqlCommand com = con.CreateCommand();
            com.CommandText = "INSERT INTO Users (username, password, email) VALUES (@username, @password, @email)";
            SqlTransaction tx = con.BeginTransaction();
            com.Transaction = tx;

            String user = usernameTextBox.Text;
            String parola = passwordTextBox.Password.ToString();
            String mail = emailTextBox.Text;

            if (valide == 1)
            {
                Users newUser = new Users(user, parola, mail);
                u.Add(newUser);

                foreach (Users us in u)
                {
                    com.Parameters.AddWithValue("@username", us.username);
                    com.Parameters.AddWithValue("@password", us.password);
                    com.Parameters.AddWithValue("@email", us.email);
                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                }
                Console.WriteLine("Tabelul Users a fost umplut");
                tx.Commit();
                con.Close();

                MessageBox.Show("User added!");

                SmtpClient Client = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "gabi36gabi36@gmail.com",
                        Password = "ascelgabi36!"
                    }
                };

                MailAddress FromEmail = new MailAddress("gabi36gabi36@gmail.com", "Events");
                MailAddress ToEmail = new MailAddress(emailTextBox.Text, "daa");

                MailMessage Message = new MailMessage()
                {
                    From = FromEmail,
                    Subject = "New account",
                    Body = "username: " + usernameTextBox.Text +"\n" + "password: " + passwordTextBox.Password.ToString(),

                };

                Message.To.Add(ToEmail);

                try
                {
                    Client.Send(Message);
                    //MessageBox.Show("Email send");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Something wrong! \n" + ex.Message, "Error");
                }


                MainWindow mn = new MainWindow();
                mn.ShowDialog();
                this.Close();
            }

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.ShowDialog();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
