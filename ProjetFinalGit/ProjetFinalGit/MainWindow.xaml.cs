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
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            GestionBD.getInstance().MainWindow = this;
            this.mainFrame.Navigate(typeof(PageTrajets));

        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            if (item.Name == "Trajets")
            {
                mainFrame.Navigate(typeof(PageTrajets));
            } else if (item.Name == "Connexion")
            {
                mainFrame.Navigate(typeof(PageConnection));
            } else if(item.Name == "GestionDesUsagers")
            {
                mainFrame.Navigate(typeof(PageGestionDesUsagers));
            }
            else if (item.Name == "GestionDeSociete")
            {
                mainFrame.Navigate(typeof(PageGestionDeLaSociete));
            }
            else if (item.Name == "monCompte")
            {
                mainFrame.Navigate(typeof(PageCompte));
            }




        }
    }
}
