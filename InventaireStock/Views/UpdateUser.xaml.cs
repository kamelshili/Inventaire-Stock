using InventaireStock.Models;
using InventaireStock.Services;
using InventaireStock.ViewModels;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUser : ContentPage
    {
        public static string Name_User { get; set; }
        public UpdateUser()
        {
            InitializeComponent();
            Name_User = "";
            Title = "ADD USER";
        }

        public UpdateUser(UserLogin userLogin)
        {
            InitializeComponent();
            UserDatabaseController userDatabaseController = new UserDatabaseController();
            var user = userDatabaseController.GetUserById(userLogin.Name_User).Result;
            txt_User.Text = user.Name_User;
            if (txt_User.Text == "Admin")
            {
                txt_User.IsEnabled = false;
                txt_Password.IsEnabled = true;
                txt_Password.Focus();
            }
            txt_Password.Text = user.Password;
            Name_User = txt_User.Text;
            Title = "UPDATE USER";


        }


        private void txt_Password_Completed(object sender, EventArgs e)
        {
            if (txt_Password.Text != null && txt_Password.Text != "")
            {
                Btn_Save.IsEnabled = true;
            }
        }

        private void Btn_Cancel_Clicked(object sender, EventArgs e)
        {
            if (txt_User.Text != "Admin")
            {
                txt_User.IsEnabled = true;
                txt_User.Text = "";
            }


            txt_Password.Text = "";
            txt_Password.IsEnabled = false;
            Btn_Save.IsEnabled = false;
            txt_User.Focus();
        }

        private async void Btn_Save_Clicked(object sender, EventArgs e)
        {

            if (txt_User.Text != null && txt_User.Text != "")
            {
                if (txt_Password.Text != null && txt_Password.Text != "")
                {
                    if (Name_User.ToLower() == "Admin".ToLower() && txt_User.Text.ToLower() != "Admin".ToLower())
                    {
                        await DisplayAlert("Error!", "Changement de username d'admin interdit ", "OK");

                    }
                    else if (Name_User.ToLower() != "Admin".ToLower() && txt_User.Text.ToLower() == "Admin".ToLower())
                    {
                        await DisplayAlert("Error!", "Username existe ", "OK");

                    }
                    else
                    {
                        UserDatabaseController userDatabaseController = new UserDatabaseController();
                        int x = 0;
                        var verifname = false;
                        if (Name_User == "")
                        {
                            Name_User = txt_User.Text;
                            verifname = true;

                        }
                        x = await userDatabaseController.SaveUser(new UserLogin(txt_User.Text, txt_Password.Text), Name_User);
                        if (x > 0)
                        {
                            if (Name_User == LoginViewModel.UserId)
                            {
                                LoginViewModel.UserId = Name_User;
                            }

                            CrossToastPopUp.Current.ShowToastMessage("Opération terminée avec succès");
                            await Navigation.PopAsync();
                        }
                    }
                }
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (txt_User.Text == "Admin")
            {
                txt_Password.Focus();
            }
            else
            {
                txt_User.Focus();
            }
        }

        private void txt_User_Completed(object sender, EventArgs e)
        {
            if (txt_User.Text != null && txt_User.Text != "")
            {
                txt_User.IsEnabled = false;

                txt_Password.IsEnabled = true;
                txt_Password.Focus();

            }
            else
            {

            }
        }

    

        private void txt_User_Focused(object sender, FocusEventArgs e)
        {
            txt_User.BackgroundColor = Colors.Yellow;
        }

        private void txt_User_Unfocused(object sender, FocusEventArgs e)
        {
            txt_User.BackgroundColor = Colors.White;

        }



        private void txt_Password_Focused(object sender, FocusEventArgs e)
        {
            txt_Password.BackgroundColor = Colors.Yellow;

        }

        private void txt_Password_Unfocused(object sender, FocusEventArgs e)
        {
            txt_Password.BackgroundColor = Colors.White;

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            if (txt_Password.Text != null && txt_Password.Text != "")
            {
                Btn_Save.IsEnabled = true;
            }
            else
            {
                Btn_Save.IsEnabled = false;

            }
        }
    }
}