using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class BonEntreeMap : EntityTypeConfiguration<BonEntree>
    {
        public BonEntreeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            ToTable("BonEntree");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.NBon).HasColumnName("NBon");
            Property(t => t.DateEntree).HasColumnName("DateEntree");
            Property(t => t.Qnt).HasColumnName("Qnt");
            Property(t => t.ArticleId).HasColumnName("ArticleId");
            Property(t => t.MagsinId).HasColumnName("MagsinId");

            // Relationships
            HasOptional(t => t.Article)
                .WithMany(t => t.BonEntrees)
                .HasForeignKey(d => d.ArticleId);
            

        }
    }
}
