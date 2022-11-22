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

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"Select * from compte where email ={e} and password ={p}";
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
            }
            catch(MySqlException ex)
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
                int retour = 0;

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into User values(null,@prenom,@email,@adresse,@email,@password) ";


                commande.Parameters.AddWithValue("@prenom", u.Prenom);
                commande.Parameters.AddWithValue("@nom", u.Nom);
                commande.Parameters.AddWithValue("@email", u.Email);
                commande.Parameters.AddWithValue("@adresse", u.Adresse);
                commande.Parameters.AddWithValue("@email", u.Email);
                commande.Parameters.AddWithValue("@password", u.Password);

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
