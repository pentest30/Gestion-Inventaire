namespace GM.Entity.Models
{
   public class CommandeLigne
    {
       public int Id { get; set; }
       public int CommandeInterneId { get; set; }
       public long ArticleId { get; set; }
       public int? Qnt { get; set; }
       public virtual Article Article { get; set; }
       public CommandeInterne CommandeInterne { get; set; }



    }
}
