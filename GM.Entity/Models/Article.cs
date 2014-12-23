using System.Collections.Generic;

namespace GM.Entity.Models
{
    public class Article
    {
      
        public long Id { get; set; }
        public string Libelle { get; set; }
        public int? CategorieId { get; set; }
        public int? SousCategorieId { get; set; }
        public int? TypeId { get; set; }
        public int? MarqueId { get; set; }
        public byte[] Image { get; set; }
        public string Discription { get; set; }
        public long? QntTotal { get;    set; }
        public long? QntMagsin { get;  set; }
        public int?  QntMin { get; set; }
        public decimal? Poids { get; set; }
        public string Code { get; set; }
        public virtual Categorie Categorie { get; set; }
        public virtual Marque Marque { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<BonEntreeLigne> BonEntreeLignes { get; set; }
      
        public virtual SousCategorie SousCategorie { get; set; }

        
       
    }
}
