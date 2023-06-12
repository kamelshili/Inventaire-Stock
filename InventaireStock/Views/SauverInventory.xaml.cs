using InventaireStock.Models;
using InventaireStock.Services;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventaireStock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SauverInventory : ContentPage
    {
        public SauverInventory()
        {
           // SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            InitializeComponent();
        }
        async private void DisplayError(string error)
        {
            await DisplayAlert("Erreur", error, "Close");
        }
        private string CreateFileCsv()
        {
            try
            {
                InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();

                var pathFile = Constants.pathExcelExport;
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                }
                StreamWriter sw = new StreamWriter(new FileStream(pathFile, FileMode.CreateNew, FileAccess.ReadWrite), Encoding.GetEncoding("iso-8859-1"));
                var list = inventaireDatabaseController.GetAllInventairesByIsRead().Result;

                sw.WriteLine("CB;site;etage;Famille;Sous famille;Marque;Modèle;Description;Etat;Bureau;Nserie;Date");

                int i = 0;
                
                foreach (var cell in list)
                {

                    string sdt = cell.DateTimeTrans;

                    sw.WriteLine(cell.CodeImmo + ";" + cell.SITEPH + ";" + cell.EMPLPH + ";" + cell.FAMILLEPH + ";" + cell.SFAMILLEPH + ";" + cell.MARQUEPH + ";" + cell.MODELEPH + ";" + cell.DescriptionPH + ";" + cell.Etat + ";" + cell.BUREAUPH + ";" + cell.SerialNumber + ";" + sdt);

                }

                sw.Close();
                //return istimerRunning;
                //});


                return pathFile;


            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
            return "error";

        }
        private string CreateWorkbook()
        {
            var workbook = new GemBox.Spreadsheet.ExcelFile();
            var worksheet = workbook.Worksheets.Add("Sheet1");
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            var list = inventaireDatabaseController.GetAllInventairesByIsRead().Result;
            int i = 2;
            worksheet.Cells["A1"].Value = "Code Immo";
            worksheet.Cells["B1"].Value = "Description";
            worksheet.Cells["C1"].Value = "SITE";
            worksheet.Cells["D1"].Value = "EMPL";
            worksheet.Cells["E1"].Value = "Etat";
            worksheet.Cells["F1"].Value = "SerialNumber";
            worksheet.Cells["G1"].Value = "Nom d'utilisateur";
            worksheet.Cells["H1"].Value = "Date";
            worksheet.Cells["I1"].Value = "Time";
            foreach (var cell in list)
            {
                worksheet.Cells["A" + i].Value = cell.CodeImmo;
                worksheet.Cells["B" + i].Value = cell.Description;
                worksheet.Cells["C" + i].Value = cell.SITE;
                worksheet.Cells["D" + i].Value = cell.EMPL;
                worksheet.Cells["E" + i].Value = cell.Etat;
                worksheet.Cells["F" + i].Value = cell.SerialNumber;
                worksheet.Cells["G" + i].Value = cell.Name_User;
                string sdt = cell.DateTimeTrans;
                if (sdt == null)
                {
                    DateTime sdtd = default;
                    sdt = sdtd.ToString();

                }
                DateTime dt = DateTime.Parse(sdt);
                worksheet.Cells["H" + i].Value = dt.ToString("dd/MM/yyyy");
                worksheet.Cells["I" + i].Value = dt.ToString("HH:mm:ss");
                i++;
            }
            var filePath = Constants.pathExcelExport;

            workbook.Save(filePath);

            return filePath;


        }

        private async void BtnS_Export_Clicked(object sender, EventArgs e)
        {
            InventaireDatabaseController inventaireDatabaseController = new InventaireDatabaseController();
            if (inventaireDatabaseController.GetCountAllInventairesByIsRead() > 0)
            {
                BtnS_Export.IsEnabled = false;
                activity.IsRunning = true;

                try
                {
                    var filePath = await Task.Run(() => CreateFileCsv());
                    CrossToastPopUp.Current.ShowToastMessage("Opération terminée avec succès ");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "Close");
                }

                activity.IsRunning = false;
                BtnS_Export.IsEnabled = true;
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage("AUCUN CODE IMMO INVENTORIE OU BASE DE DONNEES VIDE");
            }
        }
    }
}