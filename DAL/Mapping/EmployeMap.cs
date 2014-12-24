using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class EmployeMap : EntityTypeConfiguration<Employe>
    {
        public EmployeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nom)
                .HasMaxLength(50);

            this.Property(t => t.Prenom)
                .HasMaxLength(50);

            this.Property(t => t.Fonction)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Employe");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nom).HasColumnName("Nom");
            this.Property(t => t.Prenom).HasColumnName("Prenom");
            this.Property(t => t.DateNaissance).HasColumnName("DateNaissance");
            this.Property(t => t.Fonction).HasColumnName("Fonction");
            this.Property(t => t.DeprtementId).HasColumnName("DeprtementId");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");

            // Relationships
            this.HasOptional(t => t.Departement)
                .WithMany(t => t.Employes)
                .HasForeignKey(d => d.DeprtementId);
            this.HasOptional(t => t.Service)
                .WithMany(t => t.Employes)
                .HasForeignKey(d => d.ServiceId);

        }
    }
}
