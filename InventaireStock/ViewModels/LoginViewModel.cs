﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventaireStock.Models;
using InventaireStock.Services;
using InventaireStock.Views;

namespace InventaireStock.ViewModels
{
    public class LoginViewModel : BaseViewModel, INotifyPropertyChanged
    {



        public event PropertyChangedEventHandler PropertyChanged;
        public List<UserLogin> allUsers;
        public UserLogin userSelected;
        UserLogin user;
        string nameuser;
        string password;
        public static string UserId { get; set; } = string.Empty;
        public static string PasswordId { get; set; } = string.Empty;


        //les propriétes ici
        public UserLogin UserSelected
        {
            get => userSelected;
            set
            {
                if (value != null)
                {
                    userSelected = value;
                    Name_User = userSelected.Name_User;
                    var args = new PropertyChangedEventArgs(nameof(UserSelected));
                    PropertyChanged?.Invoke(this, args);
                }

            }
        }


        public List<UserLogin> AllUsers
        {
            get => allUsers;
            set
            {
                if (allUsers != value)
                {
                    allUsers = value;
                    var args = new PropertyChangedEventArgs(nameof(AllUsers));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public UserLogin User
        {
            get => user;
            set
            {
                user = new UserLogin(value);
                var args = new PropertyChangedEventArgs(nameof(User));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string Name_User
        {
            get => nameuser;
            set
            {
                nameuser = value;
                // UserId = nameuser;
                var args = new PropertyChangedEventArgs(nameof(Name_User));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                //     PasswordId = password;
                var args = new PropertyChangedEventArgs(nameof(Password));
                PropertyChanged?.Invoke(this, args);
            }
        }

        async void getListUsers()
        {
            AllUsers = new List<UserLogin>();
            UserDatabaseController userDatabaseController = new UserDatabaseController();
            AllUsers = await userDatabaseController.GetAllUsers();

        }
        public LoginViewModel()
        {
            getListUsers();
            LoginCommand = new Command(OnLoginClicked, ValidateString);
            this.PropertyChanged +=
                    (_, __) => LoginCommand.ChangeCanExecute();
           
        }

        private bool ValidateString(object arg)
        {
            return !String.IsNullOrWhiteSpace(Name_User) && !String.IsNullOrWhiteSpace(Password);
        }

        private async void OnLoginClicked(object obj)
        {
            if (App.LicenceValide)
            {
                if (UserSelected != null)
                {
                    Name_User = UserSelected.Name_User;
                    //   await userdatabasecontroller.Login(Name_User, Password);
                    user = new UserLogin(Name_User, Password);
                    User = user;
                    if (user.checkInformation())
                    {
                        UserDatabaseController userDatabaseController = new UserDatabaseController();
                        if (userDatabaseController.Login(Name_User, Password) != null)
                        {
                            UserId = Name_User;
                            PasswordId = Password;
                            Name_User = "";
                            Password = "";
                            nameuser = "";
                            password = "";
                            userSelected = new UserLogin();
                            UserSelected = new UserLogin();
                            Application.Current.MainPage = new NavigationPage(new MyHome());

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("LOGIN", "ATTENTION: MOT DE PASSE INCORRECTE!", "Ok");
                            Password = "";
                            if (obj != null)
                            {
                                ((Entry)obj).Focus();
                            }
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("LOGIN", "ATTENTION: MOT DE PASSE EST VIDE!", "Ok");
                        Password = "";
                        if (obj != null)
                        {
                            ((Entry)obj).Focus();
                        }
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Login", "ATTENTION: MOT DE PASSE EST VIDE!", "Ok");
                    Password = "";
                    if (obj != null)
                    {
                        ((Entry)obj).Focus();
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Attention ", "Version Demo!", "Ok");
            }
        }

        public Command LoginCommand { get; }
        public Command QuitCommand { get; }
    }
}

