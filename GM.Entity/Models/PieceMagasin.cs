using System;

namespace GM.Entity.Models
{
    public class PieceMagasin : IMovement
    {
        public long Id { get; set; }
        public long? PieceId { get; set; }
        // public long? BonEntreeId { get; set; }
        public int? MagasinId { get; set; }
        public long? BonEntreeId { get; set; }
        public long? ArticleId { get; set; }
        //public string PieceNInventaire { get; set; }
        public bool Disponibilite { get; set; }

        public DateTime Date { get; set; }
        public Article Article { get; set; }
        public Piece Piece { get; set; }
        public BonEntree BonEntree { get; set; }
        public Magasin Magasin { get; set; }
    }
}
