using InventaireStock.ViewModels;
using InventaireStock.Services;
using Plugin.Toast;

namespace InventaireStock.Views;

public partial class DeleteInventory : ContentPage
{
	public DeleteInventory()
	{
		InitializeComponent();
        Entry_Password.Focus();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Entry_Password.Focus();
    }

    private async  void Btn_Clear_Clicked(object sender, EventArgs e)
    {
        if (Entry_Password.Text == null || Entry_Password.Text == "")
        {
            await DisplayAlert("Not Correct", "Attention! Mot de passe vide ", "Ok");
        }
        else
        {
            UserDatabaseController userDatabaseController = new UserDatabaseController();
            var user = userDatabaseController.Login(LoginViewModel.UserId, Entry_Password.Text);
            if (user == null || user.Result == null)
                await DisplayAlert("Not Correct", "Attention! Mot de passe incorrecte ", "Ok");
            else
            {
                var action = await DisplayAlert("Delete?", " Êtes - vous sûr de vider les inventaires physique ", "Oui", "Non");
                if (action)
                {

                    InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                    if (inventaireDatabaseController.GetCountAllInventaires() > 0)
                    {

                        int x = await inventaireDatabaseController.DeleteAllInventairesByIsRead();
                        if (x > 0)
                        {
                            CrossToastPopUp.Current.ShowToastMessage("Opération terminée avec succès");
                            Entry_Password.Text = "";

                        }
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Aucune Localisation dans la base");
                        Entry_Password.Text = "";

                    }
                }
            }
        }
    }
}