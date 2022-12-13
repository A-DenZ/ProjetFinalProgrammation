using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
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


        private async void butCsv_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(GestionBD.getInstance().MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);
            

            picker.SuggestedFileName = "User";
            picker.FileTypeChoices.Add("Fichier csv", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            List<User> liste = GestionBD.getInstance().getUser().ToList();

            // La fonction ToString de la classe Client retourne: nom + ";" + prenom

            await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToCsv()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

            }
            catch(Exception ex)
            {
                
            }
        }
    }
}
