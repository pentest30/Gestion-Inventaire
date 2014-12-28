using System;

namespace GM.Entity.Models
{
    public class PieceEmployee : IMovement
    {
        public long Id { get; set; }
        public long? PieceId { get; set; }
        public long? BonSortieId { get; set; }
        //public string PieceNInventaire { get; set; }
        public DateTime Date { get; set; }
        public int DepartementId { get; set; }
       
        public int? ServiceId { get; set; }
        public int? SousServiceId { get; set; }
        public bool Utilisation { get; set; }
        public string Etat { get; set; }
        public  Departement Departement { get; set; }
        public  SousService SousService { get; set; }
        public  Piece Piece { get; set; }
        public  Service Service { get; set; }
        public BonSortie BonSortie { get; set; }
    }
}
