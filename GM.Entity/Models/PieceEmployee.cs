using System;

namespace GM.Entity.Models
{
    public class PieceEmployee : IMovement
    {
        public long Id { get; set; }
        public long? PieceId { get; set; }

        public string PieceNInventaire { get; set; }
        public DateTime Date { get; set; }
       
        public int? ServiceId { get; set; }
        public int? SousServiceId { get; set; }
        public string Discription { get; set; }
        public string Etat { get; set; }
        
        public virtual SousService SousService { get; set; }
        public virtual Piece Piece { get; set; }
        public virtual Service Service { get; set; }
    }
}
