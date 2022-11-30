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

        //    if (accValide != true)
        //    {
        //        accErreur.Text = "un champ est invalide";
        //    }
        //    else
        //    {
        //        int shit = setUser();

        //        if (shit > 0)
        //        {
        //            this.Frame.Navigate(typeof(PageTrajets));
        //        }
        //        else
        //        {
        //            this.Frame.Navigate(typeof(PageConnection));
        //        }
        //    }

        }

        //private int setUser()
        //{
        //    User u = new User()
        //    {
        //        Prenom = accPrenom.Text,
        //        Nom = accNom.Text,
        //        Phone = accPhone.Text,
        //        Adresse = accAdresse.Text,
        //        Email = accEmail.Text,
        //        Type = getType(),
        //        Password = accPassword.Password.ToString(),

        //    };

        //    return GestionBD.getInstance().addUser(u);
        //}

        private string getType()
        {
            if (cbb.SelectedIndex == 0)
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
        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageGestionDesUsagers));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            u = (User)e.Parameter;
        }





    }
}
