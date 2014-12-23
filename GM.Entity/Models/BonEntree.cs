using System;

namespace GM.Entity.Models
{
    public class BonEntree
    {
      
        public long Id { get; set; }
        public long? NBon { get; set; }
        public DateTime? DateEntree { get; set; }
        public int? MagasinId { get; set; }
        
      
        public virtual Magasin Magasin { get; set; }
       
     
    }
}
