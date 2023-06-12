using InventaireStock.Models;
using InventaireStock.Services;
using InventaireStock.ViewModels;
using Plugin.Toast;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VInventaire : ContentPage
    {
        public string Site { get; set; }
        public string Empl { get; set; }
        public string Bureau { get; set; }

        public static string Description { get; set; }
        public static string Famille { get; set; }
        public static string SFamille { get; set; }
        public static string Marque { get; set; }
        public static string Modele { get; set; }
        public static int QTY { get; set; }
        public static bool IsRead { get; set; }


        public VInventaire(string site, string empl, string bureau)
        {
            InitializeComponent();
            this.BindingContext = new InventaireViewModel();

            this.Site = site;
            this.Empl = empl;
            this.Bureau = bureau;


            List<string> listEtat = new List<string>();
            listEtat.Add("Fonctionnel");
            listEtat.Add("Non Fonctionnel");
            listEtat.Add("Bon");
            listEtat.Add("Mauvais");
            listEtat.Add("En Panne");
            Picker_Etat.ItemsSource = listEtat;
            Picker_Etat.SelectedItem = "Fonctionnel";
            //TestCamera(null, EventArgs.Empty);
        }

        public VInventaire()
        {
        }
        //private async void TestCamera(Object Sender, EventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(Entry_Immo.Text))
        //    {
        //        await cameraView.StartCameraAsync();
        //    }
        //    else 
        //    { 
        //        await cameraView.StopCameraAsync(); 
        //    }
        //}
        private async void Entry_Immo_Completed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Entry_Immo.Text))
            {



                InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                if (inventaireDatabaseController.GetCountInventaire(Entry_Immo.Text) > 0)
                {
                    if (inventaireDatabaseController.GetCountInventaireByIsRead(Entry_Immo.Text) > 0)
                    {
                        // await cameraView.StartCameraAsync();
                        var inv = await inventaireDatabaseController.GetInventaireById(Entry_Immo.Text);
                        string sdt = inv.DateTimeTrans;
                        var action = await DisplayAlert("Attention!", "CODE IMMO DEJA INVENTORIE ! \n  " + inv.Name_User + " _ " + inv.SITEPH + " _ " + inv.EMPLPH + " _ " + inv.BUREAUPH + " _ " + sdt + " \n" + "  Êtes - vous sûr de vouloir modifier", "Oui", "Non");


                        if (action)
                        {
                            inv = inventaireDatabaseController.GetInventaireById(Entry_Immo.Text).Result;
                            // Entry_Description.Text = inv.DescriptionPH;
                            Entry_Famille.Text = inv.FAMILLEPH;
                            Entry_SFamille.Text = inv.SFAMILLEPH;
                            Entry_Marque.Text = inv.MARQUEPH;
                            Entry_Modele.Text = inv.MODELEPH;
                            Picker_Etat.SelectedItem = inv.Etat;
                            Entry_NSerie.Text = inv.SerialNumber;
                            if (String.IsNullOrEmpty(Entry_Immo.Text))
                            {
                                Entry_Immo.Focus();
                            }
                            //else if (String.IsNullOrEmpty(Entry_Description.Text))
                            //{
                            //    Entry_Description.Focus();
                            //}
                            else if (String.IsNullOrEmpty(Entry_Famille.Text))
                            {
                                Entry_Famille.Focus();
                            }
                            else if (String.IsNullOrEmpty(Entry_SFamille.Text))
                            {
                                Entry_SFamille.Focus();
                            }
                            else if (String.IsNullOrEmpty(Entry_Marque.Text))
                            {
                                Entry_Marque.Focus();
                            }
                            else if (String.IsNullOrEmpty(Entry_Modele.Text))
                            {
                                Entry_Modele.Focus();
                            }
                            else
                            {
                                Entry_NSerie.Focus();
                            }
                        }
                        else
                        {
                            Entry_Immo.Text = "";
                            Entry_Immo.Focus();
                        }
                    }
                    else
                    {

                        var inv = inventaireDatabaseController.GetInventaireById(Entry_Immo.Text).Result;
                        // Entry_Description.Text = inv.Description;
                        Entry_Famille.Text = inv.FAMILLE;
                        Entry_SFamille.Text = inv.SFAMILLE;
                        Entry_Marque.Text = inv.MARQUE;
                        Entry_Modele.Text = inv.MODELE;
                        Entry_NSerie.Text = inv.SerialNumber;
                        if (String.IsNullOrEmpty(Entry_Immo.Text))
                        {
                            Entry_Immo.Focus();
                        }
                        //else if (String.IsNullOrEmpty(Entry_Description.Text))
                        //{
                        //    Entry_Description.Focus();
                        //}
                        else if (String.IsNullOrEmpty(Entry_Famille.Text))
                        {
                            Entry_Famille.Focus();
                        }
                        else if (String.IsNullOrEmpty(Entry_SFamille.Text))
                        {
                            Entry_SFamille.Focus();
                        }
                        else if (String.IsNullOrEmpty(Entry_Marque.Text))
                        {
                            Entry_Marque.Focus();
                        }
                        else if (String.IsNullOrEmpty(Entry_Modele.Text))
                        {
                            Entry_Modele.Focus();
                        }
                        else
                        {
                            Entry_NSerie.Focus();
                        }
                    }


                }
                //    else
                //    {
                //        var action = await DisplayAlert("Attention!", "Code immo N'existe Pas Dans La Base! Êtes - vous sûr de vouloir créer ?", "Oui", "Non");
                //        if (action)
                //        {
                //            if (String.IsNullOrEmpty(Entry_Immo.Text))
                //            {
                //                Entry_Immo.Focus();
                //            }
                //            //else if (String.IsNullOrEmpty(Entry_Description.Text))
                //            //{
                //            //    Entry_Description.Focus();
                //            //}
                //            else if (String.IsNullOrEmpty(Entry_Famille.Text))
                //            {
                //                Entry_Famille.Focus();
                //            }
                //            else if (String.IsNullOrEmpty(Entry_SFamille.Text))
                //            {
                //                Entry_SFamille.Focus();
                //            }
                //            else if (String.IsNullOrEmpty(Entry_Marque.Text))
                //            {
                //                Entry_Marque.Focus();
                //            }
                //            else if (String.IsNullOrEmpty(Entry_Modele.Text))
                //            {
                //                Entry_Modele.Focus();
                //            }
                //            else
                //            {
                //                Entry_NSerie.Focus();
                //            }
                //        }
                //        else
                //        {
                //            Entry_Immo.Text = "";
                //            Entry_Immo.Focus();
                //        }
                //    }



            }
        }
        private void Entry_Description_Completed(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Entry_Description.Text))
            {

            }
        }
        private void Entry_Famille_Completed(object sender, EventArgs e)
        {

        }
        private void Entry_Modele_Completed(object sender, EventArgs e)
        {

        }
        private void Entry_Marque_Completed(object sender, EventArgs e)
        {
        }
        private void Entry_SFamille_Completed(object sender, EventArgs e)
        {

        }
        private void Picker_Etat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Btn_Save_Clicked(object sender, EventArgs e)
        {
            var dt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();

            //Inventaire inventaire = new Inventaire(Entry_Immo.Text, "", "", "", "", "", "", "", Picker_Etat.SelectedItem.ToString(), "", Entry_NSerie.Text, "", this.Site, this.Empl, this.Bureau, Entry_Description.Text, Entry_Famille.Text, Entry_SFamille.Text, Entry_Marque.Text, Entry_Modele.Text, dt, LoginViewModel.UserId, true, "");
            Inventaire inventaire = new Inventaire(Entry_Immo.Text, "", "", "", "", "", "", "", Picker_Etat.SelectedItem.ToString(), "", Entry_NSerie.Text, "", this.Site, this.Empl, this.Bureau, Entry_Description.Text, Entry_Famille.Text, Entry_SFamille.Text, Entry_Marque.Text, Entry_Modele.Text, dt, LoginViewModel.UserId, true, "", QTY);
            if (inventaireDatabaseController.GetCountInventaire(Entry_Immo.Text) > 0)
            {
                //var inv1= new Inventaire(Entry_Immo.Text, Entry_Description.Text, null, null, Picker_Etat.SelectedItem.ToString(), Entry_NSerie.Text, dt, LoginViewModel.UserId, this.Site, this.Empl, true);
                var inv = inventaireDatabaseController.GetInventaireById(Entry_Immo.Text).Result;
                inventaire.ID = inv.ID;
                inventaire.SITE = inv.SITE;
                inventaire.EMPL = inv.EMPL;
                inventaire.BUREAU = inv.BUREAU;
                inventaire.Description = inv.Description;
                inventaire.FAMILLE = inv.FAMILLE;
                inventaire.SFAMILLE = inv.SFAMILLE;
                inventaire.MARQUE = inv.MARQUE;
                inventaire.MODELE = inv.MODELE;
                inventaire.DateMiseService = inv.DateMiseService;
                inventaire.QTY = inv.QTY;
                inventaire.IsRead = inv.IsRead;


            }
            int x = inventaireDatabaseController.SaveInventaire(inventaire).Result;
            if (x > 0)
            {
                CrossToastPopUp.Current.ShowToastMessage("Succes, Inventaire Enregistrée avec succées");

                onCancel();
            }
        }



        public void onCancel()
        {
            Entry_Immo.Text = string.Empty;
            Description = string.Empty;
            Famille = string.Empty;
            SFamille = string.Empty;
            Marque = string.Empty;
            Modele = string.Empty;
            //  Entry_Description.Text = string.Empty;
            Entry_Famille.Text = string.Empty;
            Entry_SFamille.Text = string.Empty;
            Entry_Marque.Text = string.Empty;
            Entry_Modele.Text = string.Empty;
            Picker_Etat.SelectedItem = "Fonctionnel";
            Entry_NSerie.Text = string.Empty;
            Entry_Immo.Text = string.Empty;
            int quantity = 1;
            Entry_Quantité.Text = quantity.ToString();
            Entry_Immo.Focus();
        }
        private void Btn_Cancel_Clicked(object sender, EventArgs e)
        {
            onCancel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            var quantite = 1;
            Entry_Quantité.Text = quantite.ToString();

            //Entry_Description.Text = Description;
            if (Famille != Entry_Famille.Text)
            {
                Entry_SFamille.Text = string.Empty;
            }
            Entry_Famille.Text = Famille;
            Entry_SFamille.Text = SFamille;
            Entry_Marque.Text = Marque;
            Entry_Modele.Text = Modele;
            if (String.IsNullOrEmpty(Entry_Immo.Text))
            {
                Entry_Immo.Focus();
            }
            //else if (String.IsNullOrEmpty(Entry_Description.Text))
            //{
            //    Entry_Description.Focus();
            //}
            else if (String.IsNullOrEmpty(Entry_Famille.Text))
            {
                Entry_Famille.Focus();
            }
            else if (String.IsNullOrEmpty(Entry_SFamille.Text))
            {
                Entry_SFamille.Focus();
            }
            else if (String.IsNullOrEmpty(Entry_Marque.Text))
            {
                Entry_Marque.Focus();
            }
            else if (String.IsNullOrEmpty(Entry_Modele.Text))
            {
                Entry_Modele.Focus();
            }
            else
            {
                Entry_NSerie.Focus();
            }
        }
        private void OnEntryFocused(Entry entry)
        {
            if (entry != null)
            {
                entry.BackgroundColor = Colors.White;
            }
            else
            {
                entry = new Entry();
                entry.BackgroundColor = Colors.White;
            }

        }


        private void OnEntryUnFocused(Entry entry)
        {
            if (entry != null)
            {
                entry.BackgroundColor = Colors.White;
            }
            else
            {
                entry = new Entry();
                entry.BackgroundColor = Colors.White;
            }
        }


        private void Entry_Immo_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private void Entry_Description_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_Description_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private void Entry_NSerie_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_NSerie_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private void Entry_Famille_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_Famille_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private void Entry_SFamille_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_SFamille_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private void Entry_Marque_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_Marque_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private void Entry_Modele_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_Modele_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        //private async void Img_Button_Filter_Clicked(object sender, EventArgs e)
        //{
        //    //Img_Button_Filter.IsEnabled = false;
        //    var desc = Entry_Description.Text;
        //    Page page = (Page)Activator.CreateInstance(typeof(ListDescriptions));
        //    await Navigation.PushAsync(page, false);
        //    Img_Button_Filter.IsEnabled = true;
        //}
        private async void Img_Button_Filter_Famille_Clicked(object sender, EventArgs e)
        {
            Img_Button_Filter_Famille.IsEnabled = false;
            var desc = Entry_Famille.Text;
            Page page = (Page)Activator.CreateInstance(typeof(ListFamille));
            await Navigation.PushAsync(page, false);
            Img_Button_Filter_Famille.IsEnabled = true;
        }
        private async void Img_Button_Filter_SFamille_Clicked(object sender, EventArgs e)
        {
            //Img_Button_Filter_SFamille.IsEnabled = false;
            var desc = Entry_SFamille.Text;
            //Page page = (Page)Activator.CreateInstance(typeof(ListSFamille));
            //await Navigation.PushAsync(page, false);
            //Img_Button_Filter_SFamille.IsEnabled = true;
        }
        private async void Img_Button_Filter_Marque_Clicked(object sender, EventArgs e)
        {
            Img_Button_Filter_Marque.IsEnabled = false;
            var desc = Entry_Marque.Text;
            //Page page = (Page)Activator.CreateInstance(typeof(ListMarque));
            //await Navigation.PushAsync(page, false);
            //Img_Button_Filter_Marque.IsEnabled = true;
        }
        private async void Img_Button_Filter_Modele_Clicked(object sender, EventArgs e)
        {
            Img_Button_Filter_Modele.IsEnabled = false;
            var desc = Entry_Modele.Text;
            //Page page = (Page)Activator.CreateInstance(typeof(ListModele));
            //await Navigation.PushAsync(page, false);
            //Img_Button_Filter_Modele.IsEnabled = true;
        }

        private async void Btn_GO_Clicked(object sender, EventArgs e)
        {
            Page page = (Page)Activator.CreateInstance(typeof(ListInventairePh));
            await Navigation.PushAsync(page, false);
        }

        private void Entry_Immo_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Entry_Immo.Text))
            {

            }
            else
            {
                Entry_Immo.Focus();
            }
        }

        private void Entry_Description_TextChanged(object sender, EventArgs e)
        {
            Description = Entry_Description.Text;

            if (!String.IsNullOrEmpty(Entry_Description.Text))
            {

            }
            else
            {

            }
        }

        private void Entry_Famille_TextChanged(object sender, EventArgs e)
        {
            Famille = Entry_Famille.Text;
        }

        private void Entry_SFamille_TextChanged(object sender, EventArgs e)
        {
            SFamille = Entry_SFamille.Text;
        }

        private void Entry_Marque_TextChanged(object sender, EventArgs e)
        {
            Marque = Entry_Marque.Text;
        }

        private void Entry_Modele_TextChanged(object sender, EventArgs e)
        {
            Modele = Entry_Modele.Text;
        }

        private void Entry_Immo_Focused(object sender, FocusEventArgs e)
        {

            OnEntryFocused(sender as Entry);
        }

        private async void Img_Button_Filter_Clicked(object sender, EventArgs e)
        {
            //Img_Button_Filter.IsEnabled = false;
            //var desc = Entry_Description.Text;
            //Page page = (Page)Activator.CreateInstance(typeof(ListDescriptions));
            //await Navigation.PushAsync(page, false);
            //Img_Button_Filter.IsEnabled = true;
        }

        private void Entry_Quantité_Focused(object sender, FocusEventArgs e)
        {
            OnEntryFocused(sender as Entry);
        }

        private void Entry_Quantité_Unfocused(object sender, FocusEventArgs e)
        {
            OnEntryUnFocused(sender as Entry);
        }

        private async void ClickButton_Clicked(object sender, EventArgs e)
        {
            My_Image.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
            await cameraView.StartCameraAsync();
            await cameraView.StopCameraAsync();
        }

        private void camera_View_CamerasLoaded(object sender, EventArgs e)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }
}