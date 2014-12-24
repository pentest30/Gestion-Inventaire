using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class BonEntreeMap : EntityTypeConfiguration<BonEntree>
    {
        public BonEntreeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("BonEntree");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NBon).HasColumnName("NBon");
            this.Property(t => t.DateEntree).HasColumnName("DateEntree");
            this.Property(t => t.Qnt).HasColumnName("Qnt");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");
            this.Property(t => t.MagsinId).HasColumnName("MagsinId");

            // Relationships
            this.HasOptional(t => t.Article)
                .WithMany(t => t.BonEntrees)
                .HasForeignKey(d => d.ArticleId);
        

        }
    }
}
