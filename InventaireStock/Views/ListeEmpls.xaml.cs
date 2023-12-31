using InventaireStock.Services;
using InventaireStock.Models;

namespace InventaireStock.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ListeEmpls : ContentPage
{
    public ListeEmpls()
    {
        InitializeComponent();

        if (VSITEEMPL.Empl != "")
        {
            Close = true;
        }
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


        if (VSITEEMPL.Empl != "")
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            int nbr = inventaireDatabaseController.GetCountInventaireByEmpl(VSITEEMPL.Empl);
            if (nbr > 0)
            {
                var inv = inventaireDatabaseController.GetInventaireByEMPL(VSITEEMPL.Empl);
                inv.EMPL = VSITEEMPL.Empl;
                listInventaire.SelectedItem = inv;

                MyInventaire = inv;
            }
        }
    }
    public async void initListInventaire()
    {
        InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
        List<Inventaire> list = await inventaireDatabaseController.GetAllInventairesEmplDistinct();

        listInventaire.ItemsSource = list;
        ListInv = list;
        Lbl_Total.Text = list.Count.ToString();
    }
    //on va changer la liste avec le contenu de txt_search si on n'�crit rien la liste sera charg�e une autre fois c'est on �crit de don�es n'existe pas dans la base la liste sera vide
    public void lstchanged(string keyword)
    {
        if (keyword == "")
        {
            listInventaire.ItemsSource = ListInv;
        }
        else
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            int nbr = inventaireDatabaseController.GetCountInventaireByEMPLFilter(keyword);
            if (nbr > 0)
            {
                listInventaire.ItemsSource =
                 ListInv.Where(i => i.EMPL != null && i.EMPL.ToLower().Contains(keyword.ToLower()));
                // var lstInventory = await inventaireDatabaseController.GetUniteBudgetaireByEmpl(keyword);
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

    //cette m�thode pour quitter la page a chaque fois on clique sur un item avec une condition de ne pas quitter si la valeur s�lectionner est null
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
            VSITEEMPL.Empl = ((Inventaire)selc).EMPL;
            //VSITEEMPL.Bureau = "";

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