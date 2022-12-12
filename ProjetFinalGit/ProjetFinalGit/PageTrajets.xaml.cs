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

        private void addTrajetBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTrajet));
        }

        private void toCSV_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void lvTrajet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.Frame.XamlRoot;
            dialog.Title = "Choisir une action";
            dialog.PrimaryButtonText = "Embarquer";
            if (GestionBD.getInstance().AccountType == "admin")
            {
                dialog.SecondaryButtonText = "Supprimer";
            }
            dialog.CloseButtonText = "Fermer";

            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
