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
        bool userLogged = false;      
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
              logErreur.Text = "Un champ est invalide";
                
            }
            if(LogPass.Password.ToString() == "")
            {
                valide = false;
                logErreur.Text = "un champ est invalide";
            }

            if(valide == true)
            {
                
               userLogged = GestionBD.getInstance().getUser(LogEmail.Text, LogPass.Password.ToString());
                if(userLogged != true)
                {
                    valide = false;
                    LogEmail.Text = "un champ est invalide";
                }
            }
        }
        private void NewUser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            logPanel.Visibility = Visibility.Collapsed;
            accPanel.Visibility = Visibility.Visible; 
        }

        private void AlreadyUser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            logPanel.Visibility = Visibility.Visible;
            accPanel.Visibility = Visibility.Collapsed;
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
