using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace InventaireStock.Models;

public class Inventaire
{
    [PrimaryKey]
    public string ID { get; set; }
    public string CodeImmo { get; set; }
    public string SITE { get; set; }
    public string EMPL { get; set; }
    public string FAMILLE { get; set; }
    public string SFAMILLE { get; set; }
    public string MARQUE { get; set; }
    public string MODELE { get; set; }
    public string Description { get; set; }
    public string Etat { get; set; }
    public string BUREAU { get; set; }
    public string SerialNumber { get; set; }
    public string TheDate { get; set; }
    public string SITEPH { get; set; }
    public string EMPLPH { get; set; }
    public string BUREAUPH { get; set; }
    public string DescriptionPH { get; set; }
    public string FAMILLEPH { get; set; }
    public string SFAMILLEPH { get; set; }
    public string MARQUEPH { get; set; }
    public string MODELEPH { get; set; }
    public string DateTimeTrans { get; set; }
    public string DateMiseService { get; set; }
    public string Name_User { get; set; }
    public bool IsRead { get; set; }
    public int QTY { get; set; }

    public Inventaire(
    string CodeImmo,
    string SITE,
    string Etage,
    string FAMILLE,
    string SFAMILLE,
    string MARQUE,
    string MODELE,
    string Description,
    string Etat,
    string BUREAU,
    string SerialNumber,
    string TheDate,
    string SITEPH,
    string EMPLPH,
    string BUREAUPH,
    string DescriptionPH,
    string FAMILLEPH,
    string SFAMILLEPH,
    string MARQUEPH,
    string MODELEPH,
    string DateTimeTrans,
    string Name_User,
    bool IsRead,
    string DateMiseService,
     int QTY
   )
    {
        this.CodeImmo = CodeImmo;
        this.SITE = SITE;
        this.EMPL = Etage;
        this.FAMILLE = FAMILLE;
        this.SFAMILLE = SFAMILLE;
        this.MARQUE = MARQUE;
        this.MODELE = MODELE;
        this.Description = Description;
        this.Etat = Etat;
        this.BUREAU = BUREAU;
        this.SerialNumber = SerialNumber;
        this.TheDate = TheDate;
        this.SITEPH = SITEPH;
        this.EMPLPH = EMPLPH;
        this.BUREAUPH = BUREAUPH;
        this.DescriptionPH = DescriptionPH;
        this.FAMILLEPH = FAMILLEPH;
        this.SFAMILLEPH = SFAMILLEPH;
        this.MARQUEPH = MARQUEPH;
        this.MODELEPH = MODELEPH;
        this.DateTimeTrans = DateTimeTrans;
        this.Name_User = Name_User;
        this.IsRead = IsRead;
        this.DateMiseService = DateMiseService;
        this.QTY = QTY;

    }
    public Inventaire()
    {

    }
}
