using InventaireStock.Models;
using InventaireStock.Services;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListInventaire : ContentPage
    {
        public ListInventaire()
        {
            InitializeComponent();
        }


        public static List<Inventaire> ListInv { get; set; } = new List<Inventaire>();
        public static bool Close { get; set; } = false;
        //on va faire les initialisation suivant avec les valeurs de la page  inventaire

        public async void initListInventaire(string site, string sousempl, string bureau)
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            List<Inventaire> list = await inventaireDatabaseController.GetInventaireByEmplCodeUB(site, sousempl, bureau);
            listInventaire.ItemsSource = list;
            ListInv = list;
            Lbl_Total.Text = list.Count.ToString();
        }
        public ListInventaire(string site, string sousempl, string bureau)
        {
            //on faire cette valeur sur false car le compilateur compile la méthode listInventaire_ItemSelected la premiére méthode et si on a exécute la méthode OnDisappearing la valeur close sera true et il ne va pas exécute la méthode listInventaire_ItemSelected  donc on initialise close avec false.
            InitializeComponent();
            initListInventaire(site, sousempl, bureau);

        }




        //cette méthode fonctionne a chaque fois la page apparait
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //on va faire une animation lors de l'ouverture de la page
        }

        //on va changer la liste avec le contenu de txt_search si on n'écrit rien la liste sera chargée une autre fois c'est on écrit de donées n'existe pas dans la base la liste sera vide
        public void lstchanged(string keyword)
        {
            if (keyword == "")
            {
                listInventaire.ItemsSource = ListInv;
            }
            else
            {
                InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                int nbr = inventaireDatabaseController.GetCountInventaireByNumImmoDescription(keyword);
                if (nbr > 0)
                {
                    listInventaire.ItemsSource =
                     ListInv.Where(i => (i.CodeImmo != null && i.CodeImmo.ToLower().Contains(keyword.ToLower())) || (i.Description != null && i.Description.ToLower().Contains(keyword.ToLower())));
                    // var lstInventory = await inventaireDatabaseController.GetUniteBudgetaireBySite(keyword);
                }
                else
                {
                    listInventaire.ItemsSource = new List<Inventaire>();
                }
            }

        }


        //private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        //{
           
        //}

        //cette méthode pour quitter la page a chaque fois on clique sur un item avec une condition de ne pas quitter si la valeur sélectionner est null
        private async void listInventaire_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selc = listInventaire.SelectedItem;
            if (selc != null && Close == false)
            {

                await Navigation.PopAsync();


            }
            else if (selc == null)
            {
            }
        }



        //cette méthode fonctionne a chaque fois quand on quitte la page avec une condition si la valeur sélectionner quand on quitte null changer la valeur MyUniteBudgetaire de la page Inventaire qui est changé dans le constrecture on faire cétte opération pour ne perdre pas aucune donnée. 
        protected override void OnDisappearing()
        {
         
            base.OnDisappearing();


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
          
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private void txt_search_Focused(object sender, FocusEventArgs e)
        {
            txt_search.BackgroundColor = Colors.Yellow;

        }

        private void txt_search_Unfocused(object sender, FocusEventArgs e)
        {
            txt_search.BackgroundColor = Colors.White;

        }
        private async void listInventaire_ItemTapped(object sender, EventArgs e)
        {
            if (listInventaire.SelectedItem != null)
            {
                var lst = listInventaire.SelectedItem as Models.Inventaire;
                await Clipboard.SetTextAsync(lst.CodeImmo);
                CrossToastPopUp.Current.ShowToastMessage(lst.CodeImmo + " Copié avec succées");

            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            var keyword = txt_search.Text;
            lstchanged(keyword);
        }

        private void listInventaire_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}