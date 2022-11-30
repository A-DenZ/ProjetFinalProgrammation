using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProjetFinalGit
{
    internal class GestionBD
    {
        MySqlConnection con;
        static GestionBD gestionBD = null;
        bool userLogged = false;

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
                commande.Parameters.Add(new MySqlParameter("@mdp", p));

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();
                if(r.Read() == true)
                {
                    userLogged = true;
                    
                    return true;     
                }
                else
                {
                    return false;
                }
                con.Close();
            }
            catch(Exception ex)
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
                string password = u.Password;
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
            catch(MySqlException ex)
            {

                con.Close();
                return 0;
            }
        }

        public bool UserLogged { get => userLogged;}

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
            while(r.Read() == true)
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
            catch(Exception ex)
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
                return true ;
            }
            catch(Exception ex)
            {
                con.Close();
                return false;
            }



            
        }


        public int UpdateUser(int i , User u )
        {
            try
            {
                string email = u.Email;
                string password = u.Password;
                string prenom = u.Prenom;
                string nom = u.Nom;
                string adresse = u.Adresse;
                string type = u.Type;
                string phone = u.Phone;

                double revenu = u.Revenu;
                




                int retour = 0;
                MySqlCommand commande = new MySqlCommand("Update_User");

                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.Add(new MySqlParameter("idObj", i));
                commande.Parameters.Add(new MySqlParameter("@firstName", prenom));
                commande.Parameters.Add(new MySqlParameter("@lastName", nom));
                commande.Parameters.Add(new MySqlParameter("@mail", email));
                commande.Parameters.Add(new MySqlParameter("@phone", phone));
                commande.Parameters.Add(new MySqlParameter("@addr", adresse));
                commande.Parameters.Add(new MySqlParameter("@mdp", password));
                commande.Parameters.Add(new MySqlParameter("@rev", revenu));
                commande.Parameters.Add(new MySqlParameter("@typeUser", type));
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();
                retour = commande.ExecuteNonQuery();

                con.Close();
                return retour;
            }
            catch(Exception ex)
            {
                con.Close();
                return 0;
            }
        }        
    

    }
}
