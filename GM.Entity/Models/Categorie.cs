using System.Collections.Generic;

namespace GM.Entity.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
