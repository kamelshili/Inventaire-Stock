namespace InventaireStock.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class VSITEEMPL : ContentPage
{

    public static string Site { get; set; }
    public static string Empl { get; set; }
    public static string Bureau { get; set; }

    public VSITEEMPL()
    {
        InitializeComponent();
        //Entry_Site.Focus();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Entry_Site.Focus();
        Entry_Site.Text = Site;
        Entry_Empl.Text = Empl;
        Entry_Bureau.Text = Bureau;
        VInventaire.Description = "";
        VInventaire.Famille = "";
        VInventaire.SFamille = "";
        VInventaire.Marque = "";
        VInventaire.Modele = "";

    }

    private async void OnNext()
    {
        if (!String.IsNullOrEmpty(Entry_Site.Text) && !String.IsNullOrEmpty(Entry_Empl.Text) && !String.IsNullOrEmpty(Entry_Bureau.Text))
        {
            Page page = (Page)Activator.CreateInstance(typeof(VInventaire), Entry_Site.Text, Entry_Empl.Text, Entry_Bureau.Text);
            await Navigation.PushAsync(page, false);
        }
    }

    private void btn_Suivant_Clicked(object sender, EventArgs e)
    {
        OnNext();
    }


    private void Entry_Site_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (!String.IsNullOrEmpty(Entry_Site.Text))
        {
            //btn_Suivant.IsEnabled = true;
            Site = Entry_Site.Text;
            //Img_Button_Filter_Empl.IsEnabled = true;
        }
        else
        {
            Entry_Empl.IsEnabled = false;
            btn_Suivant.IsEnabled = false;
            btn_Affiche.IsEnabled = false;

            //Img_Button_Filter_Empl.IsEnabled = false;

        }


        if (!String.IsNullOrEmpty(Entry_Site.Text) && !String.IsNullOrEmpty(Entry_Empl.Text) && !String.IsNullOrEmpty(Entry_Bureau.Text))
        {
            btn_Suivant.IsEnabled = true;
            btn_Affiche.IsEnabled = true;
            Site = Entry_Site.Text;
            //Img_Button_Filter_Empl.IsEnabled = true;
        }
        else
        {
            Entry_Empl.IsEnabled = false;
            btn_Suivant.IsEnabled = false;
            btn_Affiche.IsEnabled = false;

        }
    }
    private void Entry_Empl_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!String.IsNullOrEmpty(Entry_Empl.Text))
        {
            Empl = Entry_Empl.Text;
            //Img_Button_Filter_Bureau.IsEnabled = true;
        }
        else
        {
            //Entry_Bureau.IsEnabled = false;
            btn_Suivant.IsEnabled = false;
            btn_Affiche.IsEnabled = false;
            //Img_Button_Filter_Bureau.IsEnabled = false;

        }


        if (!String.IsNullOrEmpty(Entry_Site.Text) && !String.IsNullOrEmpty(Entry_Empl.Text) && !String.IsNullOrEmpty(Entry_Bureau.Text))
        {
            btn_Suivant.IsEnabled = true;
            btn_Affiche.IsEnabled = true;
            Empl = Entry_Empl.Text;
            //Img_Button_Filter_Empl.IsEnabled = true;
        }
        else
        {
            //Entry_Bureau.IsEnabled = false;
            btn_Suivant.IsEnabled = false;
            btn_Affiche.IsEnabled = false;

        }

    }

    private void Entry_Site_Completed(object sender, EventArgs e)
    {
        if (Entry_Site.Text != null && Entry_Site.Text != "" && Entry_Site.Text.Length > 0)
        {
            Entry_Empl.IsEnabled = true;
            Entry_Empl.Focus();
        }
    }

    private void Entry_Empl_Completed(object sender, EventArgs e)
    {

    }

    private void Entry_Site_Focused(object sender, FocusEventArgs e)
    {
        Entry_Site.BackgroundColor = Colors.Yellow;
    }

    private void Entry_Site_Unfocused(object sender, FocusEventArgs e)
    {
        Entry_Site.BackgroundColor = Colors.White;
    }

    private void Entry_Empl_Focused(object sender, FocusEventArgs e)
    {
        Entry_Empl.BackgroundColor = Colors.Yellow;

    }

    private void Entry_Empl_Unfocused(object sender, FocusEventArgs e)
    {
        Entry_Empl.BackgroundColor = Colors.White;

    }
    private void Entry_Bureau_Focused(object sender, FocusEventArgs e)
    {
        Entry_Bureau.BackgroundColor = Colors.Yellow;

    }
    private void Entry_Bureau_Unfocused(object sender, FocusEventArgs e)
    {
        Entry_Bureau.BackgroundColor = Colors.White;

    }



    private async void btn_Affiche_Clicked(object sender, EventArgs e)
    {
        var site = Entry_Site.Text;

        var empl = Entry_Empl.Text;
        var bureau = Entry_Bureau.Text;

        Page page = (Page)Activator.CreateInstance(typeof(ListInventaire), site, empl, bureau);
        await Navigation.PushAsync(page, false);
    }
    private async void Img_Button_Filter_Site_Clicked(object sender, EventArgs e)
    {
       
        Page page = (Page)Activator.CreateInstance(typeof(ListSites));
        await Navigation.PushAsync(page, false);
     
    }

    private async void Img_Button_Filter_Empl_Clicked(object sender, EventArgs e)
    {
        
        Page page = (Page)Activator.CreateInstance(typeof(ListeEmpls));
        await Navigation.PushAsync(page, false);
        
    }
    private async void Img_Button_Filter_Bureau_Clicked(object sender, EventArgs e)
    {
       
        Page page = (Page)Activator.CreateInstance(typeof(ListBureau));
        await Navigation.PushAsync(page, false);
    }

    private void Entry_Bureau_Completed(object sender, EventArgs e)
    {

    }

    private void Entry_Site_TextChanged(object sender, EventArgs e)
    {

    }

    private void Entry_Empl_TextChanged(object sender, EventArgs e)
    {

    }

    private void Entry_Bureau_TextChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Entry_Bureau.Text))
        {
            btn_Suivant.IsEnabled = true;
            btn_Affiche.IsEnabled = true;
            Bureau = Entry_Bureau.Text;
        }
        else
        {
            btn_Suivant.IsEnabled = false;
            btn_Affiche.IsEnabled = false;

        }

        if (!String.IsNullOrEmpty(Entry_Site.Text) && !String.IsNullOrEmpty(Entry_Empl.Text) && !String.IsNullOrEmpty(Entry_Bureau.Text))
        {
            btn_Suivant.IsEnabled = true;
            btn_Affiche.IsEnabled = true;
            Bureau = Entry_Bureau.Text;
            //Img_Button_Filter_Empl.IsEnabled = true;
        }
        else
        {
            btn_Suivant.IsEnabled = false;
            btn_Affiche.IsEnabled = false;

        }
    }
}