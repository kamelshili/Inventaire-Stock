using InventaireStock.Models;
using InventaireStock.Services;
using Plugin.Toast;

namespace InventaireStock.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ManagementUser : ContentPage
{
    public static List<UserLogin> ListUtilis { get; set; } = new List<UserLogin>();
    public ManagementUser()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        UserDatabaseController userDatabaseController = new UserDatabaseController();
        ListUtilis = userDatabaseController.GetAllUsers().Result;
        listUsers.ItemsSource = ListUtilis;

    }
    public void lstchanged(string keyword)
    {
        if (keyword == "")
        {
            listUsers.ItemsSource = ListUtilis;
        }
        else
        {
            UserDatabaseController userDatabaseController = new UserDatabaseController();
            int nbr = userDatabaseController.GetCountUserByIDSearch(keyword);
            if (nbr > 0)
            {
                listUsers.ItemsSource =
                 ListUtilis.Where(i => i.Name_User.ToLower().Contains(keyword.ToLower()));
            }
            else
            {
                listUsers.ItemsSource = new List<Item>();
            }
        }

    }

    private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = txt_search.Text;
        lstchanged(keyword);
    }

    async void Onadduser()
    {

        Page page = (Page)Activator.CreateInstance(typeof(UpdateUser));

        await Navigation.PushAsync(page, false);

    }

    async void Onupdateuser()
    {
        if (listUsers.SelectedItem != null)
        {
            Page page = (Page)Activator.CreateInstance(typeof(UpdateUser), (UserLogin)listUsers.SelectedItem);

            await Navigation.PushAsync(page, false);


        }
    }

    private void listUsers_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (listUsers.SelectedItem != null)
        {
            Btn_UpdateUser.IsEnabled = true;
            Btn_DeleteUser.IsEnabled = true;
        }
    }

    private void listUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void Btn_AddUser_Clicked(object sender, EventArgs e)
    {
        Onadduser();
        listUsers.SelectedItem = null;
        Btn_UpdateUser.IsEnabled = false;
        Btn_DeleteUser.IsEnabled = false;
    }

    private void Btn_UpdateUser_Clicked(object sender, EventArgs e)
    {
        Onupdateuser();
        listUsers.SelectedItem = null;
        Btn_UpdateUser.IsEnabled = false;
        Btn_DeleteUser.IsEnabled = false;
    }

    private async void Btn_DeleteUser_Clicked(object sender, EventArgs e)
    {
        if (listUsers.SelectedItem != null)
        {

            UserDatabaseController userDatabaseController = new UserDatabaseController();
            var user = ((UserLogin)listUsers.SelectedItem);
            if (user.Name_User.ToLower() != "Admin".ToLower())
            {
                var action = await DisplayAlert("Delete?", " L'utilisateur va supprimer, êtes-vous sûr de supprimer l'utilisateur :  " + user.Name_User, "Oui", "Non");
                if (action)
                {
                    int x = await userDatabaseController.DeleteUser(user.Name_User);
                    if (x > 0)
                    {
                        CrossToastPopUp.Current.ShowToastMessage("Opération terminée avec succès");
                        ListUtilis = userDatabaseController.GetAllUsers().Result;
                        listUsers.ItemsSource = ListUtilis;
                        listUsers.SelectedItem = null;
                        Btn_UpdateUser.IsEnabled = false;
                        Btn_DeleteUser.IsEnabled = false;

                    }
                }
            }
            else
            {
                await DisplayAlert("Error!", "Suppression d'admin interdit ", "OK");


            }
        }
    }

    private void txt_search_TextChanged(object sender, EventArgs e)
    {

    }
}