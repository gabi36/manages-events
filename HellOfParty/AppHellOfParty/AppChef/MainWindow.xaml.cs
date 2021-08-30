using AppChef.Tables;
using System;
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
using AppChef.Pages;



namespace AppChef
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Orase[] o = { new Orase(1, "Cluj-Napoca"),
                                     new Orase(2, "Bucuresti"),
                                     new Orase(3, "Timisoara"),
                                     new Orase(4, "Sadova"),
                                     new Orase(5, "Deva")
                                    };

        private static Party[] p = { new Party(100, "Glow in the CRUSH", "After Eight", 250, 1),
                                     new Party(101, "Girl's Night OUT", "Phi 18", 150, 1),
                                     new Party(200, "El Dictator", "El Grande Comandante", 300, 2),
                                     new Party(201, "Take Me To Church", "Silver Church", 350, 2),
                                     new Party(300, "College Party", "Like Pub", 250, 3),
                                     new Party(301, "Student Madness", "Heaven", 350, 3),
                                     new Party(400, "Electronic Hunt Session", "Ciuperca BTT", 150, 4),
                                     new Party(500, "Saturday Vibes", "Reno Music Club", 250, 5)
                                     };

        public static string util = "";
        public static string util1 = "";

        static private void CreareaDataBase()
        {
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true");
            con.Open();
            SqlCommand cmd = new SqlCommand("CREATE DATABASE CHEF", con);

            try
            {
                cmd.ExecuteNonQuery(); //trimitem fraza SQL spre baza de date
                Console.WriteLine("Baza de date CHEF a fost creata.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                con.ChangeDatabase("CHEF");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                cmd.CommandText = "CREATE TABLE Orase (codO int, numeOras text NOT NULL, PRIMARY KEY (codO))";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tabelul Orase a fost creat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                cmd.CommandText = "CREATE TABLE Party (codP int, nume text NOT NULL, club text NOT NULL, locuri int NOT NULL, codO int NOT NULL, PRIMARY KEY (codP), FOREIGN KEY (codO) REFERENCES Orase(codO))";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tabelul Party a fost creat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                cmd.CommandText = "CREATE TABLE Users (codU int IDENTITY(1,1) NOT NULL, username text NOT NULL, password text NOT NULL, email text NOT NULL,  PRIMARY KEY (codU))";
                cmd.ExecuteNonQuery();

                Console.WriteLine("Tabelul Users a fost creat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            try
            {
                cmd.CommandText = "CREATE TABLE Conectare (codC int IDENTITY(1,1) NOT NULL, codU int, codP int, PRIMARY KEY (codC), FOREIGN KEY (codP) REFERENCES Party(codP), FOREIGN KEY (codU) REFERENCES Users(codU), UNIQUE(codP, codU))";
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tabelul Conectare a fost creat.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void PopuleazaTabele()
        {
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            SqlCommand com = con.CreateCommand();
            com.CommandText = "INSERT INTO Orase (codO, numeOras) VALUES (@codO, @numeOras)";
            SqlTransaction tx = con.BeginTransaction();
            com.Transaction = tx;

            try
            {
                for (int i = 0; i < o.Length; i++)
                {
                    com.Parameters.AddWithValue("@codO", o[i].codO);
                    com.Parameters.AddWithValue("@numeOras", o[i].numeOras);
                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                }
                Console.WriteLine("Tabelul Orase a fost umplut");

                com.CommandText = "INSERT INTO Party (codP, nume, club, locuri, codO) VALUES (@codP, @nume, @club, @locuri, @codO)";
                for (int i = 0; i < p.Length; i++)
                {
                    com.Parameters.AddWithValue("@codP", p[i].codP);
                    com.Parameters.AddWithValue("@nume", p[i].nume);
                    com.Parameters.AddWithValue("@club", p[i].club);
                    com.Parameters.AddWithValue("@locuri", p[i].locuri);
                    com.Parameters.AddWithValue("@codO", p[i].codO);
                    com.ExecuteNonQuery();
                    com.Parameters.Clear();
                }
                Console.WriteLine("Tabelul Party a fost umplut");
                tx.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tx.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            CreareaDataBase();
            PopuleazaTabele();
            
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Register registrationWindow = new Register();
            registrationWindow.ShowDialog();
            this.Hide();
        }

        private void rememberBe()
        {
            if(Properties.Settings.Default.UserName != string.Empty)
            {
                usernameTextBox.Text = Properties.Settings.Default.UserName;
                passwordTextBox.Password = Properties.Settings.Default.Password;
            }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //rememberBe();
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSql = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSql, con);

            SqlDataReader read = com.ExecuteReader();

            String[] nume = new String[100];
            String[] parola = new String[100];
            String[] email = new String[100];

            int i = 0, ok=0;

            while (read.Read())
            {
                nume[i] = (read["username"].ToString());
                parola[i] = (read["password"].ToString());
                email[i] = (read["email"].ToString());
                i++;
            }
            read.Close();

            for(i=0;i<nume.Length && ok==0;i++)
            {
                if (usernameTextBox.Text == nume[i] && passwordTextBox.Password.ToString() == parola[i])
                {
                    util = nume[i];
                    util1 = email[i];
                    PartiesPage pp = new PartiesPage();

                    Interfata interfata = new Interfata();
                    interfata.ShowDialog();
                    this.Hide();
 
                    ok = 1;
                }
            }
            if(ok==0)
            {
                   MessageBox.Show("Incorrect username or password ");
            }
            
            if(CheckBoxRemember.IsChecked==true)
            {
                Properties.Settings.Default.UserName = usernameTextBox.Text;
                Properties.Settings.Default.Password = passwordTextBox.Password.ToString();
                Properties.Settings.Default.Save();
            }

            if(CheckBoxRemember.IsChecked==false)
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
