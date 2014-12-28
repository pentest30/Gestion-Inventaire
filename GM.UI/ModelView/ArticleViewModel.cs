using System;

namespace GM.UI.ModelView
{
   public class ArticleViewModel
    {
       public string SousCategorie { get; set; }
       public string Model { get; set; }
       public int Qnt { get; set; }
       public int QntMin { get; set; }
       public string Magasin { get; set; }
       public string Code { get; set; }

       public DateTime DateTime
       {
           get
           {
               return DateTime.Now.Date;
               
           }
       }

    }
}
