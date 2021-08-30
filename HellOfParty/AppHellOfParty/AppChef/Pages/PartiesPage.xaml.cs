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
using AppChef.Tables;


namespace AppChef.Pages
{
    /// <summary>
    /// Interaction logic for PartiesPage.xaml
    /// </summary>
    
    public partial class PartiesPage : Page
    {
        string utilizatorCurent;

        public PartiesPage()
        {
            InitializeComponent();
            utilizatorCurent = MainWindow.util;
            afisareUtilizator();
        }

        public void afisareUtilizator()
        {
            labelUtil.Content = utilizatorCurent.ToString();
        }

        private void click_Glow(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 100;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList(); 

            while(readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for(int j=0;j<nume.Count;j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
            
         
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 101;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 200;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 201;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 300;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 301;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 500;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Event added to favorite");

            int idEven = 400;

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlUsers = "SELECT * FROM Users";
            SqlCommand com = new SqlCommand(selectSqlUsers, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            SqlDataReader readUsers = com.ExecuteReader();

            ArrayList nume = new ArrayList();
            ArrayList idUser = new ArrayList();

            while (readUsers.Read())
            {
                nume.Add(readUsers["username"].ToString());
                idUser.Add(int.Parse(readUsers["codU"].ToString()));
            }
            readUsers.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            ArrayList idParty = new ArrayList();

            while (readParty.Read())
            {
                idParty.Add(int.Parse(readParty["codP"].ToString()));
            }
            readParty.Close();

            SqlCommand com3 = con.CreateCommand();
            com3.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com3.Transaction = tx;

            for (int j = 0; j < nume.Count; j++)
            {
                for (int i = 0; i < idParty.Count; i++)
                {
                    if (idParty[i].Equals(idEven) && nume[j].Equals(utilizatorCurent))
                    {
                        Conectare connect = new Conectare(int.Parse(idUser[j].ToString()), int.Parse(idParty[i].ToString()));

                        com3.Parameters.AddWithValue("@codU", connect.codU);
                        com3.Parameters.AddWithValue("@codP", connect.codP);
                        com3.ExecuteNonQuery();
                        com3.Parameters.Clear();

                        tx.Commit();
                        con.Close();
                    }
                }
            }
        }
    }
}
