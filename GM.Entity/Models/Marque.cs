using System.Collections.Generic;

namespace GM.Entity.Models
{
    public sealed class Marque
    {
        public Marque()
        {
            Articles = new List<Article>();
          
        }

        public int Id { get; set; }
        public string Libelle { get; set; }
        public ICollection<Article> Articles { get; set; }
       
    }
}
