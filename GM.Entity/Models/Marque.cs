using System.Collections.Generic;

namespace GM.Entity.Models
{
    public partial class Marque
    {
        public Marque()
        {
            this.Articles = new List<Article>();
          
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
       
    }
}
