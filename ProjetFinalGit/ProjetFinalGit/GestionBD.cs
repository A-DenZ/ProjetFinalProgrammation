using Microsoft.UI.Xaml;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Windows.System.Profile;

namespace ProjetFinalGit
{
    internal class GestionBD
    {

        const String cle = "AdminHashKey1234";
        MySqlConnection con;
        static GestionBD gestionBD = null;
        bool userLogged = false;

        int id = 0;
        string accountType = "none";

        Window mainWindow;



        public GestionBD()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq14;Uid=1979090;Pwd=1979090;");
        }

        public static GestionBD getInstance()
        {
            if (gestionBD == null)
                gestionBD = new GestionBD();

            return gestionBD;
        }

        // LES REQUETES AVEC LA BD VONT ICI
        // L'OBSERVABLE COLLECTION AUSSI !!





        public bool getUser(string e, string p)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("Login_User");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.Add(new MySqlParameter("@mail", e));
                commande.Parameters.Add(new MySqlParameter("@mdp", genererSHA256(p)));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                if (r.Read() == true)
                {
                    this.Id = r.GetInt32("id");
                    this.AccountType = r.GetString("type");

                    return true;
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        public int addUser(User u)
        {
            try
            {
                string prenom = u.Prenom;
                string nom = u.Nom;
                string phone = u.Phone;
                string adresse = u.Adresse;
                string email = u.Email;
                string password = genererSHA256(u.Password);
                string type = u.Type;
                int retour = 0;

                MySqlCommand commande = new MySqlCommand("Insert_User");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.Add(new MySqlParameter("@prenom1", prenom));
                commande.Parameters.Add(new MySqlParameter("@nom1", nom));
                commande.Parameters.Add(new MySqlParameter("@email1", email));
                commande.Parameters.Add(new MySqlParameter("@numDeTel1", phone));
                commande.Parameters.Add(new MySqlParameter("@adresse1", adresse));
                commande.Parameters.Add(new MySqlParameter("@password1", password));
                commande.Parameters.Add(new MySqlParameter("@type1", type));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                commande.Prepare();
                retour = commande.ExecuteNonQuery();

                con.Close();
                return retour;
            }
            catch (MySqlException ex)
            {

                con.Close();
                return 0;
            }
        }

        public bool UserLogged { get => userLogged; }
        public int Id { get => id; set => id = value; }
        public string AccountType { get => accountType; set => accountType = value; }
        public Window MainWindow { get => mainWindow; set => mainWindow = value; }

        ObservableCollection<User> listeUser = new ObservableCollection<User>();


        public ObservableCollection<User> getUser()
        {
            try
            {
                listeUser.Clear();
                MySqlCommand commande = new MySqlCommand("Get_User");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read() == true)
                {
                    User unUser = new User()
                    {
                        Id = r.GetInt32("id"),
                        Email = r.GetString("email"),
                        Password = r.GetString("password"),
                        Prenom = r.GetString("prenom"),
                        Nom = r.GetString("nom"),
                        Adresse = r.GetString("adresse"),
                        Type = r.GetString("type"),
                        Phone = r.GetString("numDeTel"),
                        Revenu = r.GetDouble("revenu"),


                    };

                    listeUser.Add(unUser);
                }


                return listeUser;



            }
            catch (Exception ex)
            {
                return listeUser;
            }

        }

        public bool delUser(int i)
        {
            try
            {
                int retour = 0;
                MySqlCommand commande = new MySqlCommand("Del_User");
                commande.Parameters.Add(new MySqlParameter("idObj", i));
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                retour = commande.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }




        }

        public int UpdateUser(int i, User u)
        {
            try
            {
                string email = u.Email;
                string password = genererSHA256(u.Password);
                string prenom = u.Prenom;
                string nom = u.Nom;
                string adresse = u.Adresse;
                string type = u.Type;
                string phone = u.Phone;

                double revenu = u.Revenu;


                int retour = 0;

                MySqlCommand commande = new MySqlCommand("Update_User");
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Connection = con;

                commande.Parameters.Add(new MySqlParameter("@idObj", i));
                commande.Parameters.Add(new MySqlParameter("@mail", email));
                commande.Parameters.Add(new MySqlParameter("@mdp", password));
                commande.Parameters.Add(new MySqlParameter("@firstName", prenom));
                commande.Parameters.Add(new MySqlParameter("@lastName", nom));
                commande.Parameters.Add(new MySqlParameter("@addr", adresse));
                commande.Parameters.Add(new MySqlParameter("@typeUser", type));
                commande.Parameters.Add(new MySqlParameter("@rev", revenu));
                commande.Parameters.Add(new MySqlParameter("@phone", phone));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                commande.Prepare();
                retour = commande.ExecuteNonQuery();

                con.Close();
                return retour;
            }
            catch (Exception ex)
            {
                con.Close();
                return 0;
            }
        }



        public string CreateTrajet(Trajet t)
        {
            string msg = "All good.";
            int newId = 0;

            try
            {
                MySqlCommand commande = new MySqlCommand("Insert_Trajet");
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Connection = con;

                commande.Parameters.Add(new MySqlParameter("@_placeD", t.PlaceD));
                commande.Parameters.Add(new MySqlParameter("@_placeA", t.PlaceA));
                commande.Parameters.Add(new MySqlParameter("@_heureD", t.HeureD));
                commande.Parameters.Add(new MySqlParameter("@_heureA", t.HeureA));
                commande.Parameters.Add(new MySqlParameter("@_arret", t.Arret));
                commande.Parameters.Add(new MySqlParameter("@_idV", t.Vehicule));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                commande.Prepare();
                bool retour = Convert.ToBoolean(commande.ExecuteNonQuery());

                // get last id
                commande.CommandText = "get_id_last_trajet";
                commande.Parameters.Clear();
                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    newId = r.GetInt32("id");
                }
                r.Close();


                // creating adding to comptetrajet
                commande.CommandText = "insert_compte_trajet";

                commande.Parameters.Clear();
                commande.Parameters.Add(new MySqlParameter("@_compteId", GestionBD.getInstance().Id));
                commande.Parameters.Add(new MySqlParameter("@_trajetId", newId));

                commande.Prepare();
                commande.ExecuteNonQuery();



                con.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                msg = ex.Message;
            }


            return msg;
        }


        public bool createCompteTrajet(int trajetId)
        {
            bool works = true;
            try
            {
                MySqlCommand commande = new MySqlCommand("insert_compte_trajet");
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Connection = con;

                commande.Parameters.Add(new MySqlParameter("@_compteId", id));
                commande.Parameters.Add(new MySqlParameter("@_trajetId", trajetId));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                commande.Prepare();
                works = Convert.ToBoolean(commande.ExecuteNonQuery());

                con.Close();
                works = true;
            }
            catch (Exception ex)
            {
                works = false;
            }

            return works;
        }

        public ObservableCollection<TrajetFullInfos> GetTrajetFullInfos()
        {
            ObservableCollection<TrajetFullInfos> newList = new ObservableCollection<TrajetFullInfos>();
            try
            {
                MySqlCommand commande = new MySqlCommand("get_active_trajets");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read() == true)
                {
                    TrajetFullInfos newTrajet = new TrajetFullInfos()
                    {
                        Id = r.GetInt32("Id"),
                        Chauffeur = r.GetString("Chauffeur"),
                        Trajet = r.GetString("Trajet"),
                        Arret = r.GetString("Arret"),
                        Date = r.GetString("Date"),
                        HeureD = r.GetString("HeureD"),
                        HeureA = r.GetString("HeureA"),
                        Vehicule = r.GetString("Vehicule"),
                        Tarif = r.GetInt32("Tarif")
                    };

                    newList.Add(newTrajet);
                }

                con.Close();

                return newList;

            } catch (Exception ex)
            {
                return newList;
            }

        }




        public ObservableCollection<String> getVilles()
        {
            ObservableCollection<String> villes = new ObservableCollection<string>();

            try
            {
                MySqlCommand commande = new MySqlCommand("get_villes");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read() == true)
                {
                    string ville = r.GetString("nom");

                    villes.Add(ville);
                }

                return villes;
            } catch (Exception ex)
            {
                return villes;
            }

        }







        public string genererSHA256(string texte)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texte));

            StringBuilder sb = new StringBuilder();
            foreach (Byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }


        public bool getOnTrajet(int _trajetInd)
        {
            bool works = true;
            try
            {
                MySqlCommand commande = new MySqlCommand("check_for_compte_trajet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.Add(new MySqlParameter("@_trajetId", GestionBD.getInstance().GetTrajetFullInfos()[_trajetInd].Id));
                commande.Parameters.Add(new MySqlParameter("@_compteId", GestionBD.getInstance().Id));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();

                commande.Prepare();
                MySqlDataReader r = commande.ExecuteReader();
                int exists = -1;
                while (r.Read())
                {
                    exists = r.GetInt32("result");
                }
                if (exists == -1) works = false;
                r.Close();

                if (exists == 0)
                {
                    commande.CommandText = "insert_compte_trajet";
                    commande.Parameters.Clear();
                    commande.Parameters.Add(new MySqlParameter("@_trajetId", GestionBD.getInstance().GetTrajetFullInfos()[_trajetInd].Id));
                    commande.Parameters.Add(new MySqlParameter("@_compteId", GestionBD.getInstance().Id));

                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();

                    commande.Prepare();
                    int result = commande.ExecuteNonQuery();

                    if (result == -1)
                    {
                        works = false;
                    }

                }
                else if (exists == 1) works = false;

                con.Close();
            }
            catch (MySqlException ex)
            {
                works = false;
            }

            return works;
        }









    }
}
