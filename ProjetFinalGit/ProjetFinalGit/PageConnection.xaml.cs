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
            reset();

            // changer le logEmail.Text

            if(LogEmail.Text.Length == 0)
            {
              valide = false;
                
            }
            if(LogPass.Password.ToString() == "")
            {
                valide = false;
            }
            if(valide != true)
            {
                logErreur.Text = "Un champ est invalide";
            }
    
            if(valide == true)
            {
                
               userLogged = GestionBD.getInstance().getUser(LogEmail.Text, LogPass.Password.ToString());
                if(userLogged != true)
                {
                    valide = false;
                    logErreur.Text = "l'email ou le mot de passe est invalide";
                }
                else
                {
                    //logErreur.Text = "Un usager à été login tamaman";
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
            bool accValide = true;
            reset();

            if(accPrenom.Text.Length == 0)
            {
                accValide = false;
            }
            if(accNom.Text.Length == 0)
            {
                accValide = false;
            }
            if (accPhone.Text.Length == 0)
            {
                accValide = false;                
            }
            if (accAdresse.Text.Length == 0)
            {
                accValide = false;
            }
            if (accAdresse.Text.Length == 0)
            {
                accValide = false; 
            }
            if (accEmail.Text.Length == 0)
            {
                accValide = false;
            }
            if(accPassword.Password.ToString() == "")
            {
                accValide = false;   
            }

            if(accValide != true)
            {
                accErreur.Text = "un champ est invalide";
            }
            else
            {
                setUser();
                this.Frame.Navigate(typeof(PageConnection));
            }

        }

        private void setUser()
        {
            User u = new User()
            {
                Prenom = accPrenom.Text,
                Nom = accNom.Text,
                Phone = accPhone.Text,
                Adresse = accAdresse.Text,
                Email = accEmail.Text,
                Password = accPassword.Password.ToString(),

            };
            GestionBD.getInstance().addUser(u);
        }


        private void reset()
        {
            accErreur.Text = "";
            logErreur.Text = "";
        }


    }

}
