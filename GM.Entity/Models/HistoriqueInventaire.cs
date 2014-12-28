using System;

namespace GM.Entity.Models
{
    public class HistoriqueInventaire
    {
        public long Id { get; set; }
        public long? PieceId { get; set; }
        public long? PieceMagasinId { get; set; }
        public long? PieceEmployeeId { get; set; }
        public long? ReformetId { get; set; }
        public PieceMagasin PieceMagasin { get; set; }
        public PieceEmployee PieceEmployee { get; set; }
        public Reformet Reformet { get; set; }
        public Piece Piece { get; set; }
        public DateTime? Date { get; set; }


    }

}
