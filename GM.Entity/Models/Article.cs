using System;
using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class Article
    {
        public Article()
        {
            this.BonEntrees = new List<BonEntree>();
           
        }

        public long Id { get; set; }
        public string Libelle { get; set; }
        public int? CategorieId { get; set; }
        public int? SousCtegorieId { get; set; }
        public int? MarqueId { get; set; }
        public byte[] Image { get; set; }
        public string Discription { get; set; }
        public long? QntTotal { get; set; }
        public long? QntMagsin { get; set; }
        public decimal? Poids { get; set; }
        public string Code { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual Marque Marque { get; set; }
      
        public virtual SousCategorie SousCategorie { get; set; }
        public virtual ICollection<BonEntree> BonEntrees { get; set; }
       
    }
}
