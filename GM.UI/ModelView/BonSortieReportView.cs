using System;

namespace GM.UI.ModelView
{
    public class BonSortieReportView
    {
        
        public long Id { get; set; }
        public long NBon { get; set; }
        public DateTime DateSortie { get; set; }
        public string Magasin { get; set; }
        //[ForeignKey("CommandeInterne")]
        public string CommandeInterne { get; set; }
        public string Departement { get; set; }
        public string Service { get; set; }
        public int Qnt { get; set; }
        public string Article { get; set; }
        public string Materiel { get; set; }
    }
}
