using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinalGit
{
    internal class Trajet
    {
        int id;
        int placeD;
        int placeA;
        DateTime heureD;
        DateTime heureA;
        bool arret;
        int vehicule;

        public Trajet(int placeD, int placeA, DateTime heureD, DateTime heureA, bool arret, int vehicule)
        {
            this.placeD = placeD;
            this.placeA = placeA;
            this.heureD = heureD;
            this.heureA = heureA;
            this.arret = arret;
            this.vehicule = vehicule;
        }

        public int Id { get => id; set => id = value; }
        public int PlaceD { get => placeD; set => placeD = value; }
        public int PlaceA { get => placeA; set => placeA = value; }
        public DateTime HeureD { get => heureD; set => heureD = value; }
        public DateTime HeureA { get => heureA; set => heureA = value; }
        public bool Arret { get => arret; set => arret = value; }
        public int Vehicule { get => vehicule; set => vehicule = value; }
    }
}
