using InventaireStock.Models;
using InventaireStock.Services;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListSites : ContentPage
    {

        public static List<Inventaire> ListInv { get; set; } = new List<Inventaire>();
        public static Inventaire MyInventaire { get; set; } = new Inventaire();
        public static bool Close { get; set; } = false;
        //on va faire les initialisation suivant avec les valeurs de la page  inventaire
        public ListSites()
        {
            InitializeComponent();
            if (Close != true)
                Close = false;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            txt_search.Focus();

            initListInventaire();
            if (VSITEEMPL.Site != "")
            {
                InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                int nbr = inventaireDatabaseController.GetCountInventaireBySite(VSITEEMPL.Site);
                if (nbr > 0)
                {
                    var inv = inventaireDatabaseController.GetInventaireBySite(VSITEEMPL.Site);
                    inv.SITE = VSITEEMPL.Site;
                    listInventaire.SelectedItem = inv;
                    //var x = ListInv.IndexOf(inv);

                    MyInventaire = inv;
                }
            }
        }
        public async void initListInventaire()
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            List<Inventaire> list = await inventaireDatabaseController.GetAllInventairesBySiteDistinct();
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
                int nbr = inventaireDatabaseController.GetCountInventaireBySITEFilter(keyword);
                if (nbr > 0)
                {
                    listInventaire.ItemsSource =
                     ListInv.Where(i => i.SITE.ToLower().Contains(keyword.ToLower()));
                    // var lstInventory = await inventaireDatabaseController.GetUniteBudgetaireBySite(keyword);
                }
                else
                {
                    listInventaire.ItemsSource = new List<Inventaire>();
                }
            }

        }

        //cette méthode pour quitter la page a chaque fois on clique sur un item avec une condition de ne pas quitter si la valeur sélectionner est null
        private void listInventaire_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

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

        private async void listInventaire_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selc = listInventaire.SelectedItem;
            var type = selc.GetType().ToString();

            if (selc != null && type == "InventaireStock.Models.Inventaire")
            {
                VSITEEMPL.Site = ((Inventaire)selc).SITE;
                //VSITEEMPL.Empl = "";
                ListeEmpls.Close = false;
                await Navigation.PopAsync();


            }
            else
            {
                Close = false;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            var keyword = txt_search.Text;
            lstchanged(keyword);
        }
    }
}
