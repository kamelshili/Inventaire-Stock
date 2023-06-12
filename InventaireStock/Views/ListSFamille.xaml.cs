using InventaireStock.Models;
using InventaireStock.Services;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListSFamille : ContentPage
    {
        public ListSFamille()
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
            txt_search1.Focus();
            initListInventaire();
            if (VInventaire.SFamille != "")
            {
                InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                int nbr = inventaireDatabaseController.GetCountInventaireBySFamille(VInventaire.SFamille);
                if (nbr > 0)
                {
                    var inv = await inventaireDatabaseController.GetInventaireBySFamille(VInventaire.SFamille);
                    listInventaire.SelectedItem = inv;
                    MyInventaire = inv;
                }
            }
        }
        public async void initListInventaire()
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            List<Inventaire> list = await inventaireDatabaseController.GetAllInventairesBySFamilleDistinct();
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
                int nbr = inventaireDatabaseController.GetCountInventaireBySFamilleFilter(keyword);
                if (nbr > 0)
                {
                    listInventaire.ItemsSource =
                     ListInv.Where(i => i.SFAMILLE.ToLower().Contains(keyword.ToLower()));
                }
                else
                {
                    listInventaire.ItemsSource = new List<Inventaire>();
                }
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
        }

        private void txt_search1_Focused(object sender, FocusEventArgs e)
        {
            txt_search1.BackgroundColor = Colors.Yellow;

        }

        private void txt_search1_Unfocused(object sender, FocusEventArgs e)
        {
            txt_search1.BackgroundColor = Colors.White;
        }

        private void txt_search1_TextChanged(object sender, EventArgs e)
        {
            var keyword = txt_search1.Text;
            lstchanged(keyword);
        }

        private async  void listInventaire1_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selc = listInventaire.SelectedItem;
            var type = selc.GetType().ToString();
            if (selc != null && type == "InventaireStock.Models.Inventaire")
            {
                VInventaire.SFamille = ((Inventaire)selc).SFAMILLE;
                await Navigation.PopAsync();
            }
            else
            {
                Close = false;
            }
        }

        private void listInventaire1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}