using System;

namespace GM.Entity.Models
{
    public class HistoriqueInventaire
    {
        public Guid CodeLocation { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public string Inventaire { get; set; }
        public string Etat { get; set; }    


    }
}
