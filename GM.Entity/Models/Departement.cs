using System.Collections.Generic;

namespace GM.Entity.Models
{
    public sealed class Departement
    {
        public Departement()
        {
            Employes = new List<Employe>();
            Magasins = new List<Magasin>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public ICollection<Employe> Employes { get; set; }
        public ICollection<Magasin> Magasins { get; set; }
    }
}
