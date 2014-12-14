using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public sealed class Piece
    {
        public Piece()
        {
            Mouvements = new List<Mouvement>();
        }

        public long? BonEntreeId { get; set; }
        public int? ArticleId { get; set; }
        public string NInventaire { get; set; }
        public string Etat { get; set; }
        public int? MagasinId { get; set; }
        public DateTime? DateEntree { get; set; }
        public BonEntree BonEntree { get; set; }
        public Magasin Magasin { get; set; }
        public ICollection<Mouvement> Mouvements { get; set; }
    }
}
