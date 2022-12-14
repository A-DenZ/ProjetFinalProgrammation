using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using Windows.Web.AtomPub;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinalGit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageTrajets : Page
    {
        public PageTrajets()
        {
            this.InitializeComponent();
            lvTrajet.ItemsSource = GestionBD.getInstance().GetTrajetFullInfos();
        }


        private async void addTrajetBtn_Click(object sender, RoutedEventArgs e)
        {
            string accType = GestionBD.getInstance().AccountType;
            if (accType == "none")
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.Frame.XamlRoot;
                dialog.Title = "Vous devez être connecté pour créer un trajet.";
                dialog.CloseButtonText = "Ok";

                await dialog.ShowAsync();
            } else if (accType == "user")
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.Frame.XamlRoot;
                dialog.Title = "Vous devez avoir un compte chauffeur pour créer un trajet.";
                dialog.CloseButtonText = "Ok";

                await dialog.ShowAsync();
            } else if (accType == "admin" || accType == "driver")
            {
                this.Frame.Navigate(typeof(AddTrajet));
            } else
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.Frame.XamlRoot;
                dialog.Title = $"Type de compte: {accType}";
                dialog.CloseButtonText = "Ok";

                await dialog.ShowAsync();
            }
        }

        private async void toCSV_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var picker = new Windows.Storage.Pickers.FileSavePicker();


                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(GestionBD.getInstance().MainWindow);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);


                picker.SuggestedFileName = "Trajets";
                picker.FileTypeChoices.Add("Fichier csv", new List<string>() { ".csv" });

                //crée le fichier
                Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                List<TrajetFullInfos> liste = GestionBD.getInstance().GetTrajetFullInfos().ToList();

                // La fonction ToString de la classe Client retourne: nom + ";" + prenom
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.TrajetsToCsv()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

            }
            catch (Exception ex)
            {

            }
        }

        private async void lvTrajet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string typeA = GestionBD.getInstance().AccountType;
            if (typeA == "admin" || typeA == "user")
            {
                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = this.Frame.XamlRoot;
                dialog.Title = "Choisir une action";
                dialog.PrimaryButtonText = "Embarquer";
                if (GestionBD.getInstance().AccountType == "admin")
                {
                    dialog.PrimaryButtonText = "Supprimer";
                }
                dialog.CloseButtonText = "Fermer";

                ContentDialogResult result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    bool worked = GestionBD.getInstance().getOnTrajet(lvTrajet.SelectedIndex);
                    if (worked)
                    {;
                        dialog.Title = "Vous venez de réserver votre place sur le trajet.";
                        dialog.PrimaryButtonText = "";
                        dialog.CloseButtonText = "Ok";

                        await dialog.ShowAsync();
                    } else {
                        dialog.Title = "Erreur dans la réservation sur le trajet.";
                        dialog.CloseButtonText = "Ok";
                        dialog.PrimaryButtonText = "";

                        await dialog.ShowAsync();
                    }
                }
            }
        }

    }
}
