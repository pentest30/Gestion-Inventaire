using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class DepartementMap : EntityTypeConfiguration<Departement>
    {
        public DepartementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Departement");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Libelle).HasColumnName("Libelle");
        }
    }
}
