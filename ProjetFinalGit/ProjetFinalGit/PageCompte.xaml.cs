// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

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
    public sealed partial class PageCompte : Page
    {
        public PageCompte()
        {
            this.InitializeComponent();

            if (GestionBD.getInstance().Id != 0)
            {
                titreCompte.Text = "Bienvenue sur la page de votre compte"; 
                type.Text = "Votre compte est de type : " + GestionBD.getInstance().AccountType.ToString();
                prenom.Text = "Prenom : " + GestionBD.getInstance().AccountPrenom.ToString();
                nom.Text = "Nom : " + GestionBD.getInstance().AccountNom.ToString();
                email.Text = "Email " + GestionBD.getInstance().AccountEmail.ToString();
                phone.Text = "Numéro de téléphone : " + GestionBD.getInstance().AccountNumDeTel.ToString();
                adresse.Text = "Adresse : " + GestionBD.getInstance().AccountAdresse.ToString();
                revenu.Text = "Revenu : " + Convert.ToString(GestionBD.getInstance().AccountRevenu.ToString()) + " $";

            }
            else
            {
                titreCompte.FontSize = 45;
                titreCompte.Text = "Vous devez vous connecter pour accèder à cette page";
                type.Text = "";
                prenom.Text = "";
                nom.Text = "";
                email.Text = "";
                phone.Text = "";
                adresse.Text = "";
                revenu.Text = "";
            }
        }
    }
}
