using System;

namespace GM.Entity.Models
{
    public partial class Mouvement
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public string Discription { get; set; }
        public string Etat { get; set; }
        public string NInventaire { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual Piece Piece { get; set; }
        public virtual Service Service { get; set; }
    }
}
