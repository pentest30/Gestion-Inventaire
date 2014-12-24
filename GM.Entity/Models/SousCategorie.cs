using System.Collections.Generic;

namespace GM.Entity.Models
{
    public class SousCategorie
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
