using System.Collections.Generic;

namespace GM.Entity.Models
{
    public  class Service
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int? DepartementId { get; set; }
        public virtual Departement Departement { get; set; }
        public virtual ICollection<SousService> SousServices { get; set; }
    }
}
