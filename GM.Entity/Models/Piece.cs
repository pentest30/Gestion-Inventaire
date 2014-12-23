using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Entity.Models
{
    public sealed class Piece
    {
        [Required]
        //[Column("Article_Id")]
        public long ArticleId { get; set; }
        [Key]
        public long Id { get; set; }
        public string NInventaire { get; set; }
        public string EtatPiece { get; set; }
        public string EtatStock { get; set; }

        public DateTime? DateFabrication { get; set; }
        public string Fabriquant { get; set; }
        public bool?  Garantissable { get; set; }
        //public string TypePeriode { get; set; }
        public int Periode { get; set; }
        public int MagasinId { get; set; }
        public long BonEntreeId { get; set; }
        public DateTime? DateEntree { get; set; }

        public Magasin Magasin { get; set; }
        public BonEntree BonEntree { get; set; }

        public Article Article { get; set; }
        [NotMapped]
        public ICollection<IMovement> Movements { get; set; }
        
    }
}
