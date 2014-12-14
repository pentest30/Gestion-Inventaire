using System.Data.Entity.ModelConfiguration;
using GM.Entity.Models;

namespace DAL.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Article");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Libelle).HasColumnName("Libelle");
            this.Property(t => t.CategorieId).HasColumnName("CategorieId");
            this.Property(t => t.SousCtegorieId).HasColumnName("SousCtegorieId");
            this.Property(t => t.MarqueId).HasColumnName("MarqueId");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Discription).HasColumnName("Discription");
            this.Property(t => t.QntTotal).HasColumnName("QntTotal");
            this.Property(t => t.QntMagsin).HasColumnName("QntMagsin");
            this.Property(t => t.Poids).HasColumnName("Poids");
            this.Property(t => t.Code).HasColumnName("Code");

            // Relationships
            this.HasOptional(t => t.Categorie)
                .WithMany(t => t.Articles)
                .HasForeignKey(d => d.CategorieId);
            this.HasOptional(t => t.Marque)
                .WithMany(t => t.Articles)
                .HasForeignKey(d => d.MarqueId);
          
            this.HasOptional(t => t.SousCategorie)
                .WithMany(t => t.Articles)
                .HasForeignKey(d => d.SousCtegorieId);

        }
    }
}
