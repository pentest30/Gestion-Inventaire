using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class SousCategorie
    {
        public SousCategorie()
        {
           // this.Articles = new List<Article>();
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
