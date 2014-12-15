using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class Employe
    {
        public Employe()
        {
            this.Mouvements = new List<Mouvement>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Nullable<System.DateTime> DateNaissance { get; set; }
        public string Fonction { get; set; }
        public Nullable<int> DeprtementId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public virtual Departement Departement { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<Mouvement> Mouvements { get; set; }
    }
}
