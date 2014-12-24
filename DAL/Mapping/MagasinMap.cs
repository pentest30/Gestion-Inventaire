using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class MagasinMap : EntityTypeConfiguration<Magasin>
    {
        public MagasinMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Libelle)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Magasin");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Libelle).HasColumnName("Libelle");
            this.Property(t => t.DeprtementId).HasColumnName("DeprtementId");

            // Relationships
            this.HasOptional(t => t.Departement)
                .WithMany(t => t.Magasins)
                .HasForeignKey(d => d.DeprtementId);

        }
    }
}
