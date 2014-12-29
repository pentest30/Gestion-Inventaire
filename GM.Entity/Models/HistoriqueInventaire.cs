using System;

namespace GM.Entity.Models
{
    public class HistoriqueInventaire
    {
        public Guid CodeLocation { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public long? PieceId { get; set; }


    }
}
