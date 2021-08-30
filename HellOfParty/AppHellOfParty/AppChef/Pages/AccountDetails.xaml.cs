using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;


namespace AppChef.Pages
{
    /// <summary>
    /// Interaction logic for AccountDetails.xaml
    /// </summary>
    public partial class AccountDetails : Page
    {

        ArrayList numeUtil = new ArrayList();
        ArrayList codUtil = new ArrayList();
        ArrayList emailUtil = new ArrayList();
        ArrayList parolaUtil = new ArrayList();

        ArrayList codConectare = new ArrayList();
        ArrayList codConectare_codU = new ArrayList();

        string utilizatorCurent;
        string emailUtilizatorCurent;

        public AccountDetails()
        {
            InitializeComponent();
            oldPass1.Visibility = Visibility.Hidden;
            oldPass2.Visibility = Visibility.Hidden;
            newPass.Visibility = Visibility.Hidden;
            textBoxOldPass3.Visibility = Visibility.Hidden;
            textBoxOldPass4.Visibility = Visibility.Hidden;
            textBoxNewPass1.Visibility = Visibility.Hidden;
            saveNewPass.Visibility = Visibility.Hidden;
            //border.Visibility = Visibility.Hidden;

            textBoxChangeUsename.Visibility = Visibility.Hidden;
            textBoxChangeEmail.Visibility = Visibility.Hidden;
            buttonChangeUsename.Visibility = Visibility.Hidden;
            buttonChangeEmail.Visibility = Visibility.Hidden;

            
            utilizatorCurent = MainWindow.util;
            emailUtilizatorCurent = MainWindow.util1;
            afisareUtilizator();
            afisare();
        }

        public void afisare()
        {
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUser = "SELECT * FROM Users";
            SqlCommand com3 = new SqlCommand(selectSqlUser, con);

            string selectSqlConectare = "SELECT * FROM Conectare";
            SqlCommand com4 = new SqlCommand(selectSqlConectare, con);

            SqlDataReader readUtil = com3.ExecuteReader();

            while (readUtil.Read())
            {
                codUtil.Add(int.Parse(readUtil["codU"].ToString()));
                numeUtil.Add(readUtil["username"].ToString());
                emailUtil.Add(readUtil["email"].ToString());
                parolaUtil.Add(readUtil["password"].ToString());
            }
            readUtil.Close();

            SqlDataReader readConectare = com4.ExecuteReader();

            while (readConectare.Read())
            {
                codConectare.Add(int.Parse(readConectare["codC"].ToString()));
                codConectare_codU.Add(int.Parse(readConectare["codU"].ToString()));
            }
            readConectare.Close();
            con.Close();
        }

        public void afisareUtilizator()
        {
            labelUsername.Content = utilizatorCurent.ToString();
            labelEmail.Content = emailUtilizatorCurent.ToString();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You are sure you want to delete the account?");

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            for (int i = 0; i < numeUtil.Count; i++)
            {

                if (numeUtil[i].ToString().Equals(labelUsername.Content.ToString()))
                {
                    int codU_nou = int.Parse(codUtil[i].ToString());

                    for (int j = 0; j < codConectare_codU.Count; j++)
                    {
                        if (int.Parse(codConectare_codU[j].ToString()) == codU_nou)
                        {
                            int codC_nou = int.Parse(codConectare[j].ToString());
                            SqlCommand com2 = new SqlCommand("DELETE FROM Conectare WHERE codC = " + codC_nou, con);
                            com2.ExecuteNonQuery();
                        }
                    }

                    SqlCommand com = new SqlCommand("DELETE FROM Users WHERE codU=" + codU_nou, con);

                    com.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Successfully deleted!");

                    
                }
            }
            MainWindow main = new MainWindow();
            main.ShowDialog();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            oldPass1.Visibility = Visibility.Visible;
            oldPass2.Visibility = Visibility.Visible;
            newPass.Visibility = Visibility.Visible;
            textBoxOldPass3.Visibility = Visibility.Visible;
            textBoxOldPass4.Visibility = Visibility.Visible;
            textBoxNewPass1.Visibility = Visibility.Visible;
            saveNewPass.Visibility = Visibility.Visible;
            //border.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textBoxChangeUsename.Visibility = Visibility.Visible;
            buttonChangeUsename.Visibility = Visibility.Visible;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            textBoxChangeEmail.Visibility = Visibility.Visible;
            buttonChangeEmail.Visibility = Visibility.Visible;
        }

        private static Regex email_validation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string nume_nou = textBoxChangeUsename.Text;
            string nume_vechi = labelUsername.Content.ToString();
            if(nume_nou.Length >0){
                for (int i = 0; i < numeUtil.Count; i++)
                {
                    if (numeUtil[i].ToString().Equals(nume_vechi))
                    {
                        int cod_schimb = int.Parse(codUtil[i].ToString());

                        SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
                        con.Open();

                        SqlCommand com = new SqlCommand("UPDATE Users SET username=@username WHERE codU=" + cod_schimb, con);

                        com.Parameters.AddWithValue("@username", nume_nou);

                        com.ExecuteNonQuery();
                        con.Close();

                        labelUsername.Content = nume_nou;
                        MessageBox.Show("Username changed successfully!");

                        

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
                        MailAddress ToEmail = new MailAddress(labelEmail.Content.ToString(), "daa");

                        MailMessage Message = new MailMessage()
                        {
                            From = FromEmail,
                            Subject = "Change account",
                            Body = "new username: " + textBoxChangeUsename.Text,

                        };

                        Message.To.Add(ToEmail);

                        try
                        {
                            Client.Send(Message);
                            //MessageBox.Show("Email send");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Something wrong! \n" + ex.Message, "Error");
                        }

                        textBoxChangeUsename.Visibility = Visibility.Hidden;
                        buttonChangeUsename.Visibility = Visibility.Hidden;
                        textBoxChangeUsename.Text = "";
                    }
                }

            }
            else
            MessageBox.Show("Enter a username");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string email_nou = textBoxChangeEmail.Text;
            string email_vechi = labelEmail.Content.ToString();

            if (validate_emailaddress.IsMatch(email_nou))
            {
                for (int i = 0; i < emailUtil.Count; i++)
                {
                    if (emailUtil[i].ToString().Equals(email_vechi))
                    {
                        int cod_schimb = int.Parse(codUtil[i].ToString());

                        SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
                        con.Open();

                        SqlCommand com = new SqlCommand("UPDATE Users SET email=@email WHERE codU=" + cod_schimb, con);

                        com.Parameters.AddWithValue("@email", email_nou);

                        com.ExecuteNonQuery();
                        con.Close();

                        labelEmail.Content = email_nou;
                        MessageBox.Show("Email changed successfully!");

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
                        MailAddress ToEmail = new MailAddress(labelEmail.Content.ToString(), "daa");

                        MailMessage Message = new MailMessage()
                        {
                            From = FromEmail,
                            Subject = "Change account",
                            Body = "new email: " + textBoxChangeEmail.Text,

                        };

                        Message.To.Add(ToEmail);

                        try
                        {
                            Client.Send(Message);
                            //MessageBox.Show("Email send");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Something wrong! \n" + ex.Message, "Error");
                        }

                        textBoxChangeEmail.Visibility = Visibility.Hidden;
                        buttonChangeEmail.Visibility = Visibility.Hidden;
                        textBoxChangeEmail.Text = "";
                    }
                }
            }
            else
            MessageBox.Show("Enter a valide email");

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
 

            string parola_noua = textBoxNewPass1.Password.ToString();
            string parola_veche = textBoxOldPass3.Password.ToString();
            string parola_veche1 = textBoxOldPass4.Password.ToString();

            int valide = 0;
            Boolean validateUpperCase, validateSpecialCharacter, validateLength, validateIsDigit;
            validateUpperCase = upperCase(parola_noua);
            validateSpecialCharacter = specialCharacter(parola_noua);
            validateLength = length(parola_noua);
            validateIsDigit = isDigit(parola_noua);
            if (validateUpperCase && validateIsDigit && validateLength && validateSpecialCharacter)
                valide = 1;

                for (int i = 0; i < parolaUtil.Count; i++)
                {
                    if (parola_veche == parola_veche1)
                    {
                        if (parolaUtil[i].ToString().Equals(parola_veche))
                        {
                        if (valide == 1)
                        {
                            int cod_schimb = int.Parse(codUtil[i].ToString());

                            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
                            con.Open();

                            SqlCommand com = new SqlCommand("UPDATE Users SET password=@password WHERE codU=" + cod_schimb, con);

                            com.Parameters.AddWithValue("@password", parola_noua);

                            com.ExecuteNonQuery();
                            con.Close();

                            MessageBox.Show("Password changed successfully!");

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
                            MailAddress ToEmail = new MailAddress(labelEmail.Content.ToString(), "daa");

                            MailMessage Message = new MailMessage()
                            {
                                From = FromEmail,
                                Subject = "Change account",
                                Body = "new password: " + textBoxNewPass1.Password.ToString(),

                            };

                            Message.To.Add(ToEmail);

                            try
                            {
                                Client.Send(Message);
                               // MessageBox.Show("Email send");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Something wrong! \n" + ex.Message, "Error");
                            }

                            oldPass1.Visibility = Visibility.Hidden;
                            oldPass2.Visibility = Visibility.Hidden;
                            newPass.Visibility = Visibility.Hidden;
                            textBoxOldPass3.Visibility = Visibility.Hidden;
                            textBoxOldPass4.Visibility = Visibility.Hidden;
                            textBoxNewPass1.Visibility = Visibility.Hidden;
                            saveNewPass.Visibility = Visibility.Hidden;
                            //border.Visibility = Visibility.Hidden;

                            textBoxNewPass1.Password = "";
                            textBoxOldPass3.Password = "";
                            textBoxOldPass4.Password = "";
                        }
                        else
                            MessageBox.Show("Password format is not good!");

                        }
                        else
                        MessageBox.Show("Password is not good");
                    }
                    else
                    MessageBox.Show("Password do not match");
                }
            
        }
        static Regex validate_emailaddress = email_validation();

        static Regex rgx = new Regex("[^A-Za-z0-9]");

      
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

    }
}

