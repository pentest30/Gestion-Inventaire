using System.Collections.Generic;

namespace GM.Entity.Models
{
    public  class Service
    {
        public Service()
        {
            Employes = new List<Employe>();
            Mouvements = new List<Mouvement>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public int? DepartementId { get; set; }
        public virtual Departement Departement { get; set; }
        public ICollection<Employe> Employes { get; set; }
        public ICollection<Mouvement> Mouvements { get; set; }
    }
}
