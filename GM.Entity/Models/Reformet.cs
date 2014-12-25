using System;

namespace GM.Entity.Models
{
   public class Reformet:IMovement
    {
       public long Id { get; set; }
       public long? PieceId { get; set; }

       public string PieceNInventaire { get; set; }

       public DateTime Date { get; set; }

       public string Etat { get; set; }
       public virtual Piece Piece { get; set; }
    }
}
