using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinalGit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddTrajet : Page
    {
        public AddTrajet()
        {
            this.InitializeComponent();
            cbbPlaceD.ItemsSource = GestionBD.getInstance().getVilles();
            cbbPlaceD.SelectedIndex = 0;
            cbbPlaceA.ItemsSource = GestionBD.getInstance().getVilles();
            cbbPlaceA.SelectedIndex = 0;

            dpHeureD.SelectedDate = DateTime.Now;
            tpHeureD.SelectedTime = DateTime.Now.TimeOfDay;
            dpHeureA.SelectedDate = DateTime.Now;
            tpHeureA.SelectedTime = DateTime.Now.TimeOfDay;

            cbbArret.ItemsSource = new ArrayList() { "Oui", "Non" };
            cbbArret.SelectedIndex = 0;
            cbbVehicule.ItemsSource = new ArrayList() { "VUS", "Berline" };
            cbbVehicule.SelectedIndex = 0;
        }

        private void addTrajetBtn_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";

            if (cbbPlaceD.SelectedIndex < 0)
            {
                msg = "Veuillez choisir une villem de départ.";
            }
            else if (cbbPlaceA.SelectedIndex < 0)
            {
                msg = "Veuillez choisir une villem de d'arrivée.";
            }
            else if (dpHeureD.SelectedDate == null)
            {
                msg = "Veuillez choisir une date de départ.";
            }
            else if (tpHeureD.SelectedTime == null)
            {
                msg = "Veuillez choisir une heure de départ.";
            }
            else if (dpHeureA.SelectedDate == null)
            {
                msg = "Veuillez choisir une date d'arrivée.";
            }
            else if (tpHeureA.SelectedTime == null)
            {
                msg = "Veuillez choisir une heure de d'arrivée.";
            }
            else if (cbbArret.SelectedIndex < 0) {
                msg = "Veuillez mentionner s'il y a un arrêt.";
            }
            else if (cbbVehicule.SelectedIndex < 0)
            {
                msg = "Veuillez choisir un type de véhicule";
            }
            else
            {
                try
                {
                    DateTime heureD = dpHeureD.Date.DateTime;
                    TimeSpan timeD = tpHeureD.Time;
                    heureD = heureD.Date.Add(timeD);
                    DateTime heureA = dpHeureA.Date.DateTime;
                    TimeSpan timeA = tpHeureA.Time;
                    heureA = heureA.Date.Add(timeA);

                    bool arret = cbbArret.SelectedIndex == 0 ? true : false;

                    Trajet newTrajet = new Trajet()
                    {
                        PlaceD = cbbPlaceD.SelectedIndex + 1,
                        PlaceA = cbbPlaceA.SelectedIndex + 1,
                        HeureD = heureD,
                        HeureA = heureA,
                        Arret = arret,
                        Vehicule = cbbVehicule.SelectedIndex + 1,
                    };

                    String works = GestionBD.getInstance().CreateTrajet(newTrajet);

                    if (works == "All good.") msg = "Un trajet à bien été créé.";
                    else msg = works;
                }
                catch (Exception ex)
                {
                    msg = "Erreur du serveur";
                }
            }


            formMsg.Text = msg;
        }
    }
}
