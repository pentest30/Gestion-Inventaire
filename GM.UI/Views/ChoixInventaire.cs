using System.ComponentModel.DataAnnotations;

namespace GM.UI.Views
{
    enum ChoixInventaire
    {
        [Display(Name =  "Genérer par l'application")]
        AutoGeneration ,
         [Display(Name = "Suiver le N° existant")]
        SuiverInventaireExistant,
         [Display(Name = " N° d'inventaire vide")]
        Vide
        
    }


}
