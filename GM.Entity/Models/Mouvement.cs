using System;

namespace GM.Entity.Models
{
    public class Mouvement
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? ServiceId { get; set; }
        public int? EmployeeId { get; set; }
        public string Discription { get; set; }
        public string Etat { get; set; }
        public string NInventaire { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual Piece Piece { get; set; }
        public virtual Service Service { get; set; }
    }
}
