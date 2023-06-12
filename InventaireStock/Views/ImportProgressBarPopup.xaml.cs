using InventaireStock.Services;
using Mopups.Pages;
using Mopups.Services;
using Plugin.Toast;
using InventaireStock.Models;
using System.Text;
using CommunityToolkit.Maui.Views;

namespace InventaireStock.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ImportProgressBarPopup : Popup
{
    static float maxValue = 1;
    static float progressmax = 15;
    static bool istimerRunning = true;
    static float progress = 0;
    int counter = 1;

    public ImportProgressBarPopup()
    {
        InitializeComponent();

        maxValue = 1;
        istimerRunning = true;
        progress = 0;
        progressmax = File.ReadLines(Models.Constants.pathExcelImport).Count() - 1;
        counter = 1;
        var FileCsvCreateFromBdUpdated = Models.Constants.pathExcelImport;
        var nonEmptyLines = File.ReadAllLines(FileCsvCreateFromBdUpdated, Encoding.GetEncoding("iso-8859-1"))
                    .Where(x => !x.Split(';')
                                 .Take(12)
                                 .All(cell => string.IsNullOrWhiteSpace(cell))
                                 ).Skip(1).ToList();
        progressmax = nonEmptyLines.Count;
        int i = 0;
        if (nonEmptyLines.Count > 0)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {

                if (i < nonEmptyLines.Count)
                {
                    var row = nonEmptyLines[i];
                    i++;
                    const char Separator = ';';
                    var StrRows = row;
                    string[] StrRowsSplit = null;
                    List<Inventaire> inventaires = new List<Inventaire>();
                    if (!string.IsNullOrEmpty(StrRows))
                    {
                        StrRows = StrRows.Replace("'", ",");
                        StrRows = StrRows.Replace("\"", "");
                        StrRowsSplit = StrRows.Split(Separator);
                        var inv = new Inventaire(StrRowsSplit[0], StrRowsSplit[1], StrRowsSplit[2], StrRowsSplit[3], StrRowsSplit[4], StrRowsSplit[5], StrRowsSplit[6], StrRowsSplit[7], "", StrRowsSplit[9], StrRowsSplit[10], "", "", "", "", "", "", "", "", "", "", "", false, "", 1);
                        //                    var inv = new Inventaire(StrRowsSplit[1],StrRowsSplit[2], StrRowsSplit[3],"","","","","","",StrRowsSplit[10], "","","",
                        //"",
                        //"",
                        //"",
                        //"",
                        //"",
                        //"",
                        //StrRowsSplit[20],
                        //StrRowsSplit[21],
                        //"",
                        //false,
                        //StrRowsSplit[22],
                        //int.Parse(StrRowsSplit[24]));

                        if (!string.IsNullOrEmpty(inv.CodeImmo))
                        {
                            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
                            inventaireDatabaseController.SaveInventaire(inv);
                        }
                        progress += maxValue / progressmax;
                        LinearProgressBar.ProgressTo(progress, 500, Easing.Linear);
                        progressLabel.Text = $"{counter}/{progressmax}";
                        counter += 1;
                    }
                    else
                    {
                        istimerRunning = false;
                        CrossToastPopUp.Current.ShowToastMessage("Opération terminée avec succès");
                        // Close();

                    }

                }
                else
                {
                    istimerRunning = false;
                    CrossToastPopUp.Current.ShowToastMessage("Opération terminée avec succès");
                    //Close();

                }
                return istimerRunning;
            });
        }

    }

    //private async void Boutton_Clicked(object sender, EventArgs e)
    //{
    //    Close();
    //}
}
