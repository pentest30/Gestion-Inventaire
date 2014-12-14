using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class MagasinMap : EntityTypeConfiguration<Magasin>
    {
        public MagasinMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Libelle)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Magasin");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Libelle).HasColumnName("Libelle");
            Property(t => t.DeprtementId).HasColumnName("DeprtementId");

            // Relationships
            HasOptional(t => t.Departement)
                .WithMany(t => t.Magasins)
                .HasForeignKey(d => d.DeprtementId);

        }
    }
}
