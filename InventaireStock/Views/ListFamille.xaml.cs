using InventaireStock.Models;
using InventaireStock.Services;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListFamille : ContentPage
    {
        public ListFamille()
        {
            InitializeComponent();
            if (VInventaire.Description != "")
            {
                Close = true;
            }
            /* if (Close != true)
                 Close = false;*/

        }
        public static List<Inventaire> ListInv { get; set; } = new List<Inventaire>();
        public static Inventaire MyInventaire { get; set; } = new Inventaire();
        public static bool Close { get; set; } = false;
        //on va faire les initialisation suivant avec les valeurs de la page  inventaire

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            txt_search.Focus();
            initListInventaire();
            if (VInventaire.Famille != "")
            {
                InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                int nbr = inventaireDatabaseController.GetCountInventaireByFamille(VInventaire.Famille);
                if (nbr > 0)
                {
                    var inv = await inventaireDatabaseController.GetInventaireByFamille(VInventaire.Famille);
                    listInventaire.SelectedItem = inv;
                    MyInventaire = inv;
                }
            }
        }
        public async void initListInventaire()
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            List<Inventaire> list = await inventaireDatabaseController.GetAllInventairesByFamilleDistinct();
            listInventaire.ItemsSource = list;
            ListInv = list;
            Lbl_Total.Text = list.Count.ToString();
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
                int nbr = inventaireDatabaseController.GetCountInventaireByFamilleFilter(keyword);
                if (nbr > 0)
                {
                    listInventaire.ItemsSource =
                     ListInv.Where(i => i.FAMILLE != null && i.FAMILLE.ToLower().Contains(keyword.ToLower()));
                }
                else
                {
                    listInventaire.ItemsSource = new List<Inventaire>();
                }
            }

        }





        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //var stackLayout = sender as StackLayout;
            //listInventaire.SelectedItem = stackLayout.BindingContext;
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

        private async void listInventaire_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selc = listInventaire.SelectedItem;
            var type = selc.GetType().ToString();
            if (selc != null && type == "InventaireStock.Models.Inventaire")
            {
                VInventaire.Famille = ((Inventaire)selc).FAMILLE;
                await Navigation.PopAsync();
            }
            else
            {
                Close = false;
            }
        }

        private void listInventaire_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

            var keyword = txt_search.Text;
            lstchanged(keyword);

        }
    }
}