using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public sealed class Employe
    {
        public Employe()
        {
            Mouvements = new List<Mouvement>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string Fonction { get; set; }
        public int? DeprtementId { get; set; }
        public int? ServiceId { get; set; }
        public Departement Departement { get; set; }
        public Service Service { get; set; }
        public ICollection<Mouvement> Mouvements { get; set; }
    }
}
