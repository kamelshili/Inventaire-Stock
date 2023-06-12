using CommunityToolkit.Maui.Views;
using InventaireStock.Models;
using Mopups.Pages;

namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportFromCsv : ContentPage
    {
        public ImportFromCsv()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (File.Exists(Constants.pathExcelImport))
            {

                // Navigation.PushAsync(new ImportProgressBarPopup());
                var importPopup = new ImportProgressBarPopup();
                this.ShowPopup(importPopup);
            }
            else
            {
                DisplayAlert("ERROR", "FILE NOT FOUND", "OK");
            }
        }

    }
           
}