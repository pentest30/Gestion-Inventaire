using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class SousCategorieMap : EntityTypeConfiguration<SousCategorie>
    {
        public SousCategorieMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SousCategorie");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Libelle).HasColumnName("Libelle");
            this.Property(t => t.CategorieId).HasColumnName("CategorieId");
        }
    }
}
