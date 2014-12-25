using System;

namespace GM.Entity.Models
{
    public interface IMovement
    {
        long Id { get; set; }
        long PieceId { get; set; }
         DateTime Date { get; set; }
         //tring Etat { get; set; }
    }
}
