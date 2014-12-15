using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class BonEntree
    {
        public BonEntree()
        {
            this.Pieces = new List<Piece>();
        }

        public long Id { get; set; }
        public Nullable<long> NBon { get; set; }
        public Nullable<System.DateTime> DateEntree { get; set; }
        public Nullable<int> Qnt { get; set; }
        public Nullable<long> ArticleId { get; set; }
        public Nullable<int> MagsinId { get; set; }
        public virtual Article Article { get; set; }
        public virtual Article Article1 { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; }
    }
}
