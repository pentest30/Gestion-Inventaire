using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Entity.Models
{
    public class CommandeInterne
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public DateTime? DateCommnde { get; set; }
        [ForeignKey("Departement")]
        public int? DepartementId { get; set; }
         [ForeignKey("Service")]
        public int?  ServiceId { get; set; }
        public Service Service { get; set; }
        public Departement Departement { get; set; }


    }
}
