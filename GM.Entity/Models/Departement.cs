using System.Collections.Generic;

namespace GM.Entity.Models
{
    public class Departement
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public virtual ICollection<Employe> Employes { get; set; }
        public virtual ICollection<Magasin> Magasins { get; set; }
    }
}
