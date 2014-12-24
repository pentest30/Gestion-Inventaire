using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Entity.Models
{
    public class BonSortieLigne
    {
        public int Id { get; set; }
        public int? Qnt { get; set; }

        [ForeignKey("Article")]
        public long? ArticleId { get; set; }

        [ForeignKey("Departement")]
        public int? DepartementId { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }

       

        [ForeignKey("BonSortie")]
        public long BonSortieId { get; set; }

        public BonSortie BonSortie { get; set; }

        public  Service Service { get; set; }
        public  Departement Departement { get; set; }
        public  Article Article { get; set; }
    }
}
