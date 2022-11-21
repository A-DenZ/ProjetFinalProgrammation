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

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"Select * from compte where email ={e} and password ={p}";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if(r.Read() == true)
            { 
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
