using AppChef.Tables;
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

namespace AppChef.Pages
{
    /// <summary>
    /// Interaction logic for LocationsPage.xaml
    /// </summary>
    public partial class LocationsPage : Page
    {
        ArrayList codOras = new ArrayList();
        ArrayList numeOras = new ArrayList();
       
        ArrayList codParty = new ArrayList();
        ArrayList numeParty = new ArrayList();
        ArrayList codOrasParty = new ArrayList();
        ArrayList numeClub = new ArrayList();
        ArrayList locuri = new ArrayList();

        ArrayList numeUtil = new ArrayList();
        ArrayList codUtil = new ArrayList();

        ArrayList codConectare = new ArrayList();
        ArrayList codConectare_codU = new ArrayList();
        ArrayList codConectare_codP = new ArrayList();

        string utilizatorCurent;


        public LocationsPage()
        {
            InitializeComponent();
            utilizatorCurent = MainWindow.util;
            afisare();
            button.Visibility = Visibility.Hidden;
        }

        private void afisare2()
        {
            listBoxParties.Items.Clear();
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlOrase = "SELECT * FROM Orase";
            SqlCommand com = new SqlCommand(selectSqlOrase, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            string selectSqlUser = "SELECT * FROM Users";
            SqlCommand com3 = new SqlCommand(selectSqlUser, con);

            string selectSqlConectare = "SELECT * FROM Conectare";
            SqlCommand com4 = new SqlCommand(selectSqlConectare, con);

            SqlDataReader readOrase = com.ExecuteReader();

            while (readOrase.Read())
            {
                numeOras.Add(readOrase["numeOras"].ToString());
                codOras.Add(int.Parse(readOrase["codO"].ToString()));
            }
            readOrase.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            while (readParty.Read())
            {
                codParty.Add(int.Parse(readParty["codP"].ToString()));
                numeParty.Add(readParty["nume"].ToString());
                codOrasParty.Add(int.Parse(readParty["codO"].ToString()));
                numeClub.Add(readParty["club"].ToString());
                locuri.Add(int.Parse(readParty["locuri"].ToString()));
            }
            readParty.Close();

            SqlDataReader readUtil = com3.ExecuteReader();

            while (readUtil.Read())
            {
                codUtil.Add(int.Parse(readUtil["codU"].ToString()));
                numeUtil.Add(readUtil["username"].ToString());
            }
            readUtil.Close();

            SqlDataReader readConectare = com4.ExecuteReader();

            while (readConectare.Read())
            {
                codConectare.Add(int.Parse(readConectare["codC"].ToString()));
                codConectare_codU.Add(int.Parse(readConectare["codU"].ToString()));
                codConectare_codP.Add(int.Parse(readConectare["codP"].ToString()));
            }
            readConectare.Close();

            con.Close();

            textBox_copiat.Visibility = System.Windows.Visibility.Hidden;
        }

        private void afisare()
        {
           
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            string selectSqlOrase = "SELECT * FROM Orase";
            SqlCommand com = new SqlCommand(selectSqlOrase, con);

            string selectSqlParty = "SELECT * FROM Party";
            SqlCommand com2 = new SqlCommand(selectSqlParty, con);

            string selectSqlUser = "SELECT * FROM Users";
            SqlCommand com3 = new SqlCommand(selectSqlUser, con);

            string selectSqlConectare = "SELECT * FROM Conectare";
            SqlCommand com4 = new SqlCommand(selectSqlConectare, con);

            SqlDataReader readOrase = com.ExecuteReader();

            while (readOrase.Read())
            {
                numeOras.Add(readOrase["numeOras"].ToString());
                codOras.Add(int.Parse(readOrase["codO"].ToString()));
            }
            readOrase.Close();

            SqlDataReader readParty = com2.ExecuteReader();

            while (readParty.Read())
            {
                codParty.Add(int.Parse(readParty["codP"].ToString()));
                numeParty.Add(readParty["nume"].ToString());
                codOrasParty.Add(int.Parse(readParty["codO"].ToString()));
                numeClub.Add(readParty["club"].ToString());
                locuri.Add(int.Parse(readParty["locuri"].ToString()));
            }
            readParty.Close();

            SqlDataReader readUtil = com3.ExecuteReader();

            while(readUtil.Read())
            {
                codUtil.Add(int.Parse(readUtil["codU"].ToString()));
                numeUtil.Add(readUtil["username"].ToString());
            }
            readUtil.Close();

            SqlDataReader readConectare = com4.ExecuteReader();

            while (readConectare.Read())
            {
                codConectare.Add(int.Parse(readConectare["codC"].ToString()));
                codConectare_codU.Add(int.Parse(readConectare["codU"].ToString()));
                codConectare_codP.Add(int.Parse(readConectare["codP"].ToString()));
            }
            readConectare.Close();

            foreach (string orase in numeOras)
            {
                listBoxOrase.Items.Add(orase);
            }

            con.Close();

            textBox_copiat.Visibility = System.Windows.Visibility.Hidden;

        }
        string name;

        private void listBoxOrase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            name = listBoxOrase.SelectedItem.ToString();
            int cod_cautat = 0;
            for(int i=0;i<numeOras.Count;i++)
            {
                if (name.Equals(numeOras[i].ToString()))
                {
                      cod_cautat = int.Parse(codOras[i].ToString());
                      listBoxParties.Items.Clear();
                }
            }

            for (int i = 0; i < numeUtil.Count; i++)
            {
                if (numeUtil[i].ToString().Equals(utilizatorCurent.ToString()))
                {
                    for (int j = 0; j < codConectare.Count; j++)
                    {
                        for (int l = 0; l < codParty.Count; l++)
                        {
                            if ((int.Parse(codConectare_codP[j].ToString()) == int.Parse(codParty[l].ToString())) && (int.Parse(codConectare_codU[j].ToString()) == int.Parse(codUtil[i].ToString())))
                            {
                                if(cod_cautat == int.Parse(codOrasParty[l].ToString()))
                                {
                                    listBoxParties.Items.Add(numeParty[l]);
                                }
                            }
                        }
                    }
                }
            }

            

        }

        private void listBoxParties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            for (int i=0;i<numeParty.Count;i++)
            {
                int poz = 0;
                if(listBoxParties.SelectedItem != null)
                {

                
                if (listBoxParties.SelectedItem.ToString().Equals(numeParty[i]))
                {
                        textBoxNume.Text = numeParty[i].ToString();
                        textBox_copiat.Text = numeParty[i].ToString();
                        poz = i;
                        int cod_gasit = int.Parse(codOrasParty[poz].ToString());

                        textBoxLocatie.Text = numeClub[poz].ToString();
                        textBoxNumarLocuri.Text = locuri[poz].ToString();

                        for (int j = 0; j < codOras.Count; j++)
                        {

                            if (int.Parse(codOras[j].ToString()) == cod_gasit)
                            {
                                textBoxOras.Text = numeOras[j].ToString();
                            }
                        }

                    }
                }
            }
        }
        public Boolean isDigit(string str)
        {
            Boolean validate = true;
            foreach (char c in str)
                if (!char.IsDigit(c))
                    validate = false;
/*            if (!validate)
                MessageBox.Show("Number of seats must be a number");*/
            return validate;
        }

        public Boolean validate(String oras, String nume, String locatie, String numar)
        {
            if (isDigit(numar))
            {
                if (oras.Length > 0 && nume.Length > 0 && locatie.Length > 0)
                    return true;
            }
            else
                MessageBox.Show("Number of seats must be a number");
            return false;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool adauga = false;
            string city = textBoxOras.Text.ToString();
            string nume = textBoxNume.Text.ToString();
            string locatie = textBoxLocatie.Text.ToString();
            string numar = textBoxNumarLocuri.Text.ToString();

            bool valide = validate(city, nume, locatie, numar);

            if (valide)
            {

                SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
                con.Open();

                int ultim_cod = 0;
                for (int j = 0; j < codParty.Count; j++)
                {
                    ultim_cod = int.Parse(codParty[j].ToString());
                }

                int cod_nou;

                SqlCommand com2 = con.CreateCommand();
                com2.CommandText = "INSERT INTO Party (codP, nume, club, locuri, codO) VALUES (@codP, @nume, @club, @locuri, @codO)";
                SqlTransaction tx = con.BeginTransaction();
                com2.Transaction = tx;

                string nume_nou = textBoxNume.Text;
                string locatie_noua = textBoxLocatie.Text;
                int locuri_noi;
                if (textBoxNumarLocuri.Text == "")
                    MessageBox.Show("Number of seats must be a number");
                else
                {
                    locuri_noi = int.Parse(textBoxNumarLocuri.Text);

                    for (int i = 0; i < numeOras.Count; i++)
                    {
                        if (textBoxOras.Text.Equals(numeOras[i]))
                        {
                            int codP_nou = ultim_cod + 1;
                            cod_nou = int.Parse(codOras[i].ToString());
                            Party party = new Party(codP_nou, nume_nou, locatie_noua, locuri_noi, cod_nou);

                            com2.Parameters.AddWithValue("@codP", party.codP);
                            com2.Parameters.AddWithValue("@nume", party.nume);
                            com2.Parameters.AddWithValue("@club", party.club);
                            com2.Parameters.AddWithValue("@locuri", party.locuri);
                            com2.Parameters.AddWithValue("@codO", party.codO);
                            com2.ExecuteNonQuery();
                            com2.Parameters.Clear();

                            //MessageBox.Show("event successfully added!");
                            adauga = true;



                        }
                    }
                }
                tx.Commit();
                con.Close();
                // afisare2();
                if (adauga == true)
                {
                    Button_Click_3(sender, e);
                    
                }
            }
            else
                MessageBox.Show("Complete all fields!");


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string city = textBoxOras.Text.ToString();
            string nume = textBoxNume.Text.ToString();
            string locatie = textBoxLocatie.Text.ToString();
            string numar = textBoxNumarLocuri.Text.ToString();

            bool valide = validate(city, nume, locatie, numar);

            if (valide)
            {
                string nume_nou = textBoxNume.Text;
                string locatie_nou = textBoxLocatie.Text;
                int locuri_noi = int.Parse(textBoxNumarLocuri.Text);

                SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
                con.Open();

                for (int i = 0; i < numeParty.Count; i++)
                {

                    if (numeParty[i].ToString().Equals(textBox_copiat.Text))
                    {
                        int codP_nou = int.Parse(codParty[i].ToString());

                        SqlCommand com = new SqlCommand("UPDATE Party SET nume=@nume, club=@club, locuri=@locuri WHERE codP=" + codP_nou, con);

                        com.Parameters.AddWithValue("@nume", nume_nou);
                        com.Parameters.AddWithValue("@club", locatie_nou);
                        com.Parameters.AddWithValue("@locuri", locuri_noi);

                        com.ExecuteNonQuery();
                        con.Close();

                        //MessageBox.Show("Event successfully modified!");

                        textBoxLocatie.Text = "";
                        textBoxNumarLocuri.Text = "";
                        textBoxNume.Text = "";
                        textBoxOras.Text = "";
                    }
                }
                // afisare2();
            }
            else
            MessageBox.Show("Complete all fields!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            for (int i = 0; i < numeParty.Count; i++)
            {

                if (numeParty[i].ToString().Equals(textBox_copiat.Text))
                {
                    int codP_nou = int.Parse(codParty[i].ToString());

                    for(int j=0;j<codConectare_codP.Count;j++)
                    {
                        if(int.Parse(codConectare_codP[j].ToString()) == codP_nou)
                        {
                            int codC_nou = int.Parse(codConectare[j].ToString());
                            SqlCommand com2 = new SqlCommand("DELETE FROM Conectare WHERE codC = " + codC_nou, con);
                            com2.ExecuteNonQuery();
                        }
                    }

                    SqlCommand com = new SqlCommand("DELETE FROM Party WHERE codP=" + codP_nou, con);

                    com.ExecuteNonQuery();
                    
                    con.Close();

                    MessageBox.Show("event successfully deleted!");
                    textBoxLocatie.Text = "";
                    textBoxNumarLocuri.Text = "";
                    textBoxNume.Text = "";
                    textBoxOras.Text = "";
                }
            }
           // afisare2();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            afisare2();
            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-ISJAOKU\WINCC; Integrated Security = true; database = CHEF");
            con.Open();

            SqlCommand com2 = con.CreateCommand();
            com2.CommandText = "INSERT INTO Conectare (codU, codP) VALUES(@codU, @codP)";
            SqlTransaction tx = con.BeginTransaction();
            com2.Transaction = tx;

            string party = textBoxNume.Text;
            int cod_party = 0;
            int cod_util = 0; 

            for(int i=0;i<numeParty.Count;i++)
            {
                if(party.Equals(numeParty[i].ToString()))
                {
                    cod_party = int.Parse(codParty[i].ToString());
                }
            }

            for(int i=0;i<numeUtil.Count;i++)
            {
                if(numeUtil[i].ToString().Equals(utilizatorCurent))
                {
                    cod_util = int.Parse(codUtil[i].ToString());
                }
            }

            Conectare conectare = new Conectare(cod_util, cod_party);

              com2.Parameters.AddWithValue("@codU", conectare.codU);
              com2.Parameters.AddWithValue("@codP", conectare.codP);
              com2.ExecuteNonQuery();
              com2.Parameters.Clear();

               MessageBox.Show("event successfully added!");
            textBoxLocatie.Text = "";
            textBoxNumarLocuri.Text = "";
            textBoxNume.Text = "";
            textBoxOras.Text = "";

            tx.Commit();
            con.Close();
            
        }

        private void TextBoxNume_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
