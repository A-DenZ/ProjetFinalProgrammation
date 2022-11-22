using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        ObservableCollection<User> listeUser = new ObservableCollection<User>();

        

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


    }
}
