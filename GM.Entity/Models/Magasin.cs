using System.Collections.Generic;

namespace GM.Entity.Models
{
    public sealed class Magasin
    {
        public Magasin()
        {
            Pieces = new List<Piece>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public int? DeprtementId { get; set; }
        public Departement Departement { get; set; }
        public ICollection<Piece> Pieces { get; set; }
    }
}
