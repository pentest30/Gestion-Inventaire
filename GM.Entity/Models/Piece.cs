using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class Piece
    {
        public Piece()
        {
            this.Mouvements = new List<Mouvement>();
        }

        public Nullable<long> BonEntreeId { get; set; }
        public Nullable<int> ArticleId { get; set; }
        public string NInventaire { get; set; }
        public string Etat { get; set; }
        public Nullable<int> MagasinId { get; set; }
        public Nullable<System.DateTime> DateEntree { get; set; }
        public virtual BonEntree BonEntree { get; set; }
        public virtual Magasin Magasin { get; set; }
        public virtual ICollection<Mouvement> Mouvements { get; set; }
    }
}
