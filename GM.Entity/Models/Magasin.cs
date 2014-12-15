using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class Magasin
    {
        public Magasin()
        {
            this.Pieces = new List<Piece>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public Nullable<int> DeprtementId { get; set; }
        public virtual Departement Departement { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; }
    }
}
