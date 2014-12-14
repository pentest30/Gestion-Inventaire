using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class PieceMap : EntityTypeConfiguration<Piece>
    {
        public PieceMap()
        {
            // Primary Key
            this.HasKey(t => t.NInventaire);

            // Properties
            this.Property(t => t.NInventaire)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Etat)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Piece");
            this.Property(t => t.BonEntreeId).HasColumnName("BonEntreeId");
            this.Property(t => t.ArticleId).HasColumnName("ArticleId");
            this.Property(t => t.NInventaire).HasColumnName("NInventaire");
            this.Property(t => t.Etat).HasColumnName("Etat");
            this.Property(t => t.MagasinId).HasColumnName("MagasinId");
            this.Property(t => t.DateEntree).HasColumnName("DateEntree");

            // Relationships
            this.HasOptional(t => t.BonEntree)
                .WithMany(t => t.Pieces)
                .HasForeignKey(d => d.BonEntreeId);
            this.HasOptional(t => t.Magasin)
                .WithMany(t => t.Pieces)
                .HasForeignKey(d => d.MagasinId);

        }
    }
}
