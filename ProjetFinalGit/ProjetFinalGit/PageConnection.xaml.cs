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
    public sealed partial class PageConnection : Page
    {
        bool UserLogged = false; 
        public PageConnection()
        {
            this.InitializeComponent();
        }

        private void LogInBut_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true; 


            // changer le logEmail.Text

            if(LogEmail.Text == "")
            {
              valide = false; 
              LogEmail.Text = "Un champ est invalide";
                
            }
            if(LogPass.Password.ToString() == "")
            {
                valide = false;
                LogEmail.Text = "un champ est invalide";
            }

            if(valide = true)
            {
                
               UserLogged = GestionBD.getInstance().getUser(LogEmail.Text, LogPass.Password.ToString());
                if(UserLogged != true)
                {
                    valide = false; 
                }
            }
        }
        private void NewUser_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
