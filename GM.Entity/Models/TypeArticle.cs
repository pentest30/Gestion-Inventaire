using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Entity.Models
{
    [Table("Types")]
    public class TypeArticle
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int? CategorieId { get; set; }
        public int? SousCategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public SousCategorie SousCategorie { get; set; }

    }
}
