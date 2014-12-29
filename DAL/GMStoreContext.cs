using System.Data.Entity;
using GM.Entity.Models;

namespace DAL
{
    public class GmStoreContext : DbContext 
    {
        static GmStoreContext()
        {
            Database.SetInitializer<GmStoreContext>(null);
        }

        public GmStoreContext()
            : base("Name=GmStoreContext")
        {
            
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<BonEntree> BonEntrees { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Marque> Marques { get; set; }
        //public DbSet<UseMouvement> Mouvements { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SousService> SousServices { get; set; }

        public DbSet<SousCategorie> SousCategories { get; set; }
        public DbSet<Reformet> ReformeMovements { get; set; }
        public DbSet<PieceMagasin> PieceMagasins { get; set; }
        public DbSet<PieceEmployee> UseMouvements { get; set; }
        public DbSet<BonEntreeLigne> BonEntreeLignes { get; set; }
        public DbSet<BonSortie> BonSorties { get; set; }
        public DbSet<BonSortieLigne> BonSortieLignes { get; set; }
        public DbSet<CommandeInterne> CommandeInternes { get; set; }
        public DbSet<CommandeLigne> CommandeLignes { get; set; }
        public DbSet<HistoriqueInventaire> HistoriqueInventaires { get; set; }
        public DbSet<TypeArticle> Types { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Piece>()
            //    .HasOptional(x=>x.HistoriqueInventaires)
            //    .WithOptionalDependent()
            //    .WillCascadeOnDelete(true);
        }
    }
}
