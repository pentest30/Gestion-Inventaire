using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Entity.Models
{
   public class BonEntreeLigne
    {
       public int Id { get; set; }
        public int? Qnt { get; set; }
       [ForeignKey("Article")]
        public long? ArticleId { get; set; }
       [ForeignKey("BonEntree")]
       public long BonEntreeId  { get; set; }
       public virtual Article Article { get; set; }
       public BonEntree BonEntree { get; set; }
       
    }
}
