using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public sealed class BonEntree
    {
        public BonEntree()
        {
            Pieces = new List<Piece>();
        }

        public long Id { get; set; }
        public long? NBon { get; set; }
        public DateTime? DateEntree { get; set; }
        public int? Qnt { get; set; }
        public long? ArticleId { get; set; }
        public int? MagsinId { get; set; }
        public Article Article { get; set; }
        public Article Article1 { get; set; }
        public ICollection<Piece> Pieces { get; set; }
    }
}
