using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinalGit
{
    internal class TrajetFullInfos
    {
        int id;
        string chauffeur;
        string trajet;
        string arret;
        string date;
        string heureD;
        string heureA;
        string vehicule;
        int tarif;

        public int Id { get => id; set => id = value; }
        public string Chauffeur { get => chauffeur; set => chauffeur = value; }
        public string Trajet { get => trajet; set => trajet = value; }
        public string Arret { get => arret; set => arret = value; }
        public string Date { get => date; set => date = value; }
        public string HeureD { get => heureD; set => heureD = value; }
        public string HeureA { get => heureA; set => heureA = value; }
        public string Vehicule { get => vehicule; set => vehicule = value; }
        public int Tarif { get => tarif; set => tarif = value; }
    }
}
