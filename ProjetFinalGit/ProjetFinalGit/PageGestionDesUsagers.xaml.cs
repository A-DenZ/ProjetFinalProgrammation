using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinalGit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageGestionDesUsagers : Page
    {
        public PageGestionDesUsagers()
        {
            this.InitializeComponent(); 
            lvUsagers.ItemsSource = GestionBD.getInstance().getUser();
        }

        private void lvUsagers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User u = lvUsagers.SelectedItem as User;

            this.Frame.Navigate(typeof(PageUpdateAndDeleteUsers),u);
        }

        // nom du boutton pour le fichier csv en haut de la barre
        private async void btLire_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            /******************** POUR WINUI3 ***************************/
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
            /************************************************************/

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

            List<User> liste = new List<User>();

            /*boucle permettant de lire chacune des lignes du fichier
            * et de remplir une liste d'objets de type CLient
            */
            foreach (var ligne in lignes)
            {
                var v = ligne.Split(";");
                liste.Add(new User { Nom = v[0], Prenom = v[1] });
            }

            //on peut mettre la liste de Clients comme source d'une listView
            lvUsagers.ItemsSource = liste;


        }



        }
    }
