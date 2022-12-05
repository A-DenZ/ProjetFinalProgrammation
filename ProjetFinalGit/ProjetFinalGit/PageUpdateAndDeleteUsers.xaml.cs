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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinalGit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageUpdateAndDeleteUsers : Page
    {
        User u;
       
        public PageUpdateAndDeleteUsers()
        {
            this.InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            bool accValide = true;
            reset();

            if (accPrenom.Text.Length == 0)
            {
                accValide = false;
            }
            if (accNom.Text.Length == 0)
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
            if (accPassword.Password.ToString() == "")
            {
                accValide = false;
            }

            if (cbb.SelectedIndex == -1)
            {
                accValide = false;
            }

            if(accRevenu.Text.Length == 0)
            {
                accValide = false;
            }





            if (accValide != true)
            {
                accErreur.Text = "un champ est invalide";
            }
            else
            {
                int shit = setUser();

                if (shit > 0)
                {
                    this.Frame.Navigate(typeof(PageGestionDesUsagers));
                }
                else
                {
                    accErreur.Text = "une erreur c'est produite " ;
                    
                }
            }

        }






        public double validateRevenu()
        {
            try
            {

                double revenu = Convert.ToDouble(accRevenu.Text);

                return revenu;


            }catch(Exception ex) 
            {
              return 0;
            }
        }


        private string getType()
        {
            if (cbb.SelectedIndex == 0)
            {
                return "user";
            }
            else if (cbb.SelectedIndex == 1)
            {
                return "driver";
            }
            else
            {
                return "admin";
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
                Revenu = validateRevenu(),
                Type = getType(),
                Password = accPassword.Password.ToString(),

            };

            int userID = u.Id;
            return GestionBD.getInstance().UpdateUser(userID,u);
        }




        // PROVENANCE DE L'AUTRE PAGE !

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {

                u = (User)e.Parameter;

                accEmail.Text = u.Email;
                accAdresse.Text = u.Adresse;
                accNom.Text = u.Nom;
                accPassword.Password = u.Password;
                accPhone.Text = u.Phone;
                accPrenom.Text= u.Prenom;
                accRevenu.Text = Convert.ToString(u.Revenu);
            

                if(u.Type == "user")
                {
                    cbb.SelectedIndex = 0;
                }else if(u.Type == "driver")
                { 
                    cbb.SelectedIndex = 1; 
                }
                else
                {
                    cbb.SelectedIndex = 2;
                }

            }catch (Exception ex)
            {
                accErreur.Text = "Une erreur c'est produite";
            }





        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
               bool failcheck =  GestionBD.getInstance().delUser(u.Id);
                if(failcheck == true)
                {
                    this.Frame.Navigate(typeof(PageGestionDesUsagers));
                }
                else 
                {
                    accErreur.Text = "La suppréssion n'a pas fonctionnée";
                }

            }catch(Exception ex) 
            {
                accErreur.Text = "La suppréssion n'a pas fonctionnée";
            }


        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageGestionDesUsagers));
        }






        private void reset()
        {
            accErreur.Text = "";
        }



    }
}
