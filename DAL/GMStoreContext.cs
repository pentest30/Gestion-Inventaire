using System.Data.Entity;
using DAL.Mapping;
using GM.Entity.Models;

namespace DAL
{
    public class GMStoreContext : DbContext
    {
        static GMStoreContext()
        {
            Database.SetInitializer<GMStoreContext>(null);
        }

        public GMStoreContext()
            : base("Name=GMStoreContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<BonEntree> BonEntrees { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Mouvement> Mouvements { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SousCategorie> SousCategories { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new BonEntreeMap());
            modelBuilder.Configurations.Add(new CategorieMap());
            modelBuilder.Configurations.Add(new DepartementMap());
            modelBuilder.Configurations.Add(new EmployeMap());
            modelBuilder.Configurations.Add(new MagasinMap());
            modelBuilder.Configurations.Add(new MarqueMap());
            modelBuilder.Configurations.Add(new MouvementMap());
            modelBuilder.Configurations.Add(new PieceMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new SousCategorieMap());
           
        }
    }
}
