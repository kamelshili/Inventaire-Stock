using InventaireStock.Models;
using InventaireStock.Services;
using InventaireStock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            UserDatabaseController userDatabaseController = new UserDatabaseController();
            if (userDatabaseController.GetCountUserByID("Admin") == 0)
            {
                int x = userDatabaseController.SaveUser(new UserLogin("Admin", "1234"), "Admin").Result;
                if (x > 0)
                {

                }
            }
            this.BindingContext = new LoginViewModel();
        }
        private async void Quit(object sender, EventArgs args)
        {
            var action = await DisplayAlert("Exit?", " L'application va fermer, �tes-vous s�r de vouloir quitter? ", "Oui", "Non");
            if (action)
            {
                System.Environment.Exit(0);
            }
        }

        private void Entry_Password_Focused(object sender, FocusEventArgs e)
        {
            Entry_Password.BackgroundColor = Colors.Yellow;

        }

        private void Entry_Password_Unfocused(object sender, FocusEventArgs e)
        {
            Entry_Password.BackgroundColor = Colors.White;

        }
        private void Btn_setting_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(App.Setting, false);
            Application.Current.MainPage = new NavigationPage(new Setting());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.LicenceValide)
            {
                Lbl_demo.IsVisible = false;
            }
        }
    }
}