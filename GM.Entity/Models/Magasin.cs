using System.Collections.Generic;

namespace GM.Entity.Models
{
    public  class Magasin
    {
       
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int? DepartementId { get; set; }
        public  Departement Departement { get; set; }
        //public ICollection<Piece> Pieces { get; set; }
    }
}
