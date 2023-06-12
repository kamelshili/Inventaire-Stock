using InventaireStock.ViewModels;
using Mopups.Interfaces;
using Mopups.Services;

namespace InventaireStock.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MyHome : ContentPage
{
	public MyHome()
	{
		InitializeComponent();
        if (LoginViewModel.UserId.ToLower() == "ADMIN".ToLower())
        {
           // Label_GestUser.IsVisible = true;
           // Frame_GestUser.IsVisible = true;
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //VSITEEMPL.Site = "";
        //VSITEEMPL.Empl = "";
        //VSITEEMPL.Bureau = "";
        ListSites.Close = false;
        ListeEmpls.Close = false;
        ListBureau.Close = false;
    }




    private void OnLogOut()
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
   
    private async void OnInventaire()
    {
        await Navigation.PushAsync(new VSITEEMPL(), false);
    }

    private void Btn_Inventaire_Clicked(object sender, EventArgs e)
    {
        OnInventaire();
    }

    private void Btn_LogOut_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
    async void Ongestionuser()
    {
        await Navigation.PushAsync(new ManagementUser(), false);
    }
    private void Btn_ManagUser_Clicked(object sender, EventArgs e)
    {
        Ongestionuser();
    }
    private async void OnImport()
    {
        await Navigation.PushAsync(new ImportFromCsv(), false);
    }

    private async void OnDelete()
    {
        await Navigation.PushAsync(new DeleteInventory(), false);
    }

    private void Btn_DeleteInventaire_Clicked(object sender, EventArgs e)
    {
        OnDelete();
    }
    private async void OnSauver()
    {
        // MainPage.VLocalisationPage = new VLocalisation();
        await Navigation.PushAsync(new SauverInventory(), false);
    }
    private void Btn_Export_Clicked(object sender, EventArgs e)
    {
        OnSauver();
    }

    private void imgbtn_Import_Clicked(object sender, EventArgs e)
    {
        OnImport();
    }
}