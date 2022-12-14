using Microsoft.UI;
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
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
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
            cbb.SelectedIndex = 0;
            if (GestionBD.getInstance().Id != 0)
            {
                logPanel.Visibility = Visibility.Collapsed;
                accPanel.Visibility = Visibility.Collapsed;
                loggedPanel.Visibility = Visibility.Visible;
            }
            
        }

        private async void LogInBut_Click(object sender, RoutedEventArgs e)
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
                    logErreur.Text = "l'email ou le mot de passe est invalide.";
                }
                else
                {
                    logErreur.Foreground = new SolidColorBrush(Colors.Green);
                    logErreur.Text = "Vous êtes maintenant connecté.";
                    await Task.Delay(750);
                    this.Frame.Navigate(typeof(PageTrajets));
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

            if(cbb.SelectedIndex == -1)
            {
                accValide = false;
            }

            if(accValide != true)
            {
                accErreur.Text = "un champ est invalide";
            }
            else
            {
                int shit = setUser();

                if (shit > 0)
                {
                    this.Frame.Navigate(typeof(PageTrajets));
                } else
                {
                    this.Frame.Navigate(typeof(PageConnection));
                }
            }

        }

        private int setUser()
        {
            User u = new User()
            {
                Prenom = accPrenom.Text,
                Nom = accNom.Text,
                Phone = accPhone.Text,
                Adresse = accAdresse.Text,
                Email = accEmail.Text,
                Type = getType(),
                Password = accPassword.Password.ToString(),

            };

            return GestionBD.getInstance().addUser(u);
        }

        private string getType()
        {
            if(cbb.SelectedIndex == 0)
            {
                return "user";
            }
            else
            {
                return "driver";
            }
        }

        private void reset()
        {
            accErreur.Text = "";
            logErreur.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GestionBD.getInstance().Id = 0;
            GestionBD.getInstance().AccountType = "";
        }
    }

}
