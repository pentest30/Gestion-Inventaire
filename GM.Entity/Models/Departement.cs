using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class Departement
    {
        public Departement()
        {
            //this.Employes = new List<Employe>();
            //this.Magasins = new List<Magasin>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public virtual ICollection<Employe> Employes { get; set; }
        public virtual ICollection<Magasin> Magasins { get; set; }
    }
}
