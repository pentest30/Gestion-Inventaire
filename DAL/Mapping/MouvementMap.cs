using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class MouvementMap : EntityTypeConfiguration<Mouvement>
    {
        public MouvementMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Etat)
                .HasMaxLength(50);

            this.Property(t => t.NInventaire)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Mouvement");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.Discription).HasColumnName("Discription");
            this.Property(t => t.Etat).HasColumnName("Etat");
            this.Property(t => t.NInventaire).HasColumnName("NInventaire");

            // Relationships
            this.HasOptional(t => t.Employe)
                .WithMany(t => t.Mouvements)
                .HasForeignKey(d => d.EmployeeId);
            this.HasOptional(t => t.Piece)
                .WithMany(t => t.Mouvements)
                .HasForeignKey(d => d.NInventaire);
            this.HasOptional(t => t.Service)
                .WithMany(t => t.Mouvements)
                .HasForeignKey(d => d.ServiceId);

        }
    }
}
