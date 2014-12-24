using System;

namespace GM.Entity.Models
{
    public class BonSortie
    {
        public long Id { get; set; }
        public long? NBon { get; set; }
        public DateTime? DateSortie { get; set; }
        public int? MagasinId { get; set; }
        //[ForeignKey("CommandeInterne")]
        public int? CommandeInterneId { get; set; }
        public CommandeInterne CommndeInterne { get; set; }

        //public virtual Article Article { get; set; }
        public virtual Magasin Magasin { get; set; }
    }
}
