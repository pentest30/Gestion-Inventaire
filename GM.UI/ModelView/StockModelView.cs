using System;

namespace GM.UI.ModelView
{
   public class StockModelView
    {
       public long Id { get; set; }
       public DateTime Date { get; set; }
       public string Inventaire { get; set; }
       public int NBon { get; set; }
       public string Model { get; set; }
       public string Categorie { get; set; }
       public string SousCategorie { get; set; }
       public string Type { get; set; }
       public string Marque { get; set; }
       public long PieceId { get; set; }
       public bool Disponibilite { get; set; }
       public string Etat { get; set; }




    }
}
