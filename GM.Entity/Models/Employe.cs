using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public class Employe
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string Fonction { get; set; }
        public int? DepartementId { get; set; }
        public int? ServiceId { get; set; }
        public virtual Departement Departement { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<PieceService> Mouvements { get; set; }
       
    }
}
