using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinalGit
{
    internal class User
    {
        string prenom;
        string nom;
        string phone;
        string adresse;
        string email;
        string password;
        string type;
        double revenu;
        int id;

        public string Prenom { get => prenom; set => prenom = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Type { get => type; set => type = value; }
        public double Revenu { get => revenu; set => revenu = value; }
        public int Id { get => id; set => id = value; }

        public string ToCsv()
        {
            return id + ";" + prenom + ";" + nom + ";" + phone + ";" + adresse + ";" + email + ";" + type + ";" + revenu + ";" ;
        }
    }
}
