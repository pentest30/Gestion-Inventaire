using System.Collections.Generic;

namespace GM.Entity.Models
{
    public class Departement
    {
        public Departement()
        {
            Services =  new List<Service>();
        }
        public int Id { get; set; }
        public string Libelle { get; set; }

       

        public  ICollection<Magasin> Magasins { get; set; }
        public  ICollection<Service> Services { get; set; }
    }
}
