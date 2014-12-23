namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Libelle = c.String(),
                        CategorieId = c.Int(),
                        SousCategorieId = c.Int(),
                        TypeId = c.Int(),
                        MarqueId = c.Int(),
                        Image = c.Binary(),
                        Discription = c.String(),
                        QntTotal = c.Long(),
                        QntMagsin = c.Long(),
                        QntMin = c.Int(),
                        Poids = c.Decimal(precision: 18, scale: 2),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategorieId)
                .ForeignKey("dbo.Marques", t => t.MarqueId)
                .ForeignKey("dbo.SousCategories", t => t.SousCategorieId)
                .ForeignKey("dbo.Types", t => t.TypeId)
                .Index(t => t.CategorieId)
                .Index(t => t.SousCategorieId)
                .Index(t => t.TypeId)
                .Index(t => t.MarqueId);
            
            CreateTable(
                "dbo.BonEntreeLignes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qnt = c.Int(),
                        ArticleId = c.Long(),
                        BonEntreeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.BonEntrees", t => t.BonEntreeId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.BonEntreeId);
            
            CreateTable(
                "dbo.BonEntrees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NBon = c.Long(),
                        DateEntree = c.DateTime(),
                        MagasinId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Magasins", t => t.MagasinId)
                .Index(t => t.MagasinId);
            
            CreateTable(
                "dbo.Magasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        DepartementId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.Departements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(),
                        Fonction = c.String(),
                        DepartementId = c.Int(),
                        ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.PieceServices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(nullable: false),
                        PieceNInventaire = c.String(),
                        Date = c.DateTime(nullable: false),
                        ServiceId = c.Int(),
                        EmployeeId = c.Int(),
                        Discription = c.String(),
                        Etat = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employes", t => t.EmployeeId)
                .ForeignKey("dbo.Pieces", t => t.PieceId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.PieceId)
                .Index(t => t.ServiceId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Pieces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArticleId = c.Long(nullable: false),
                        NInventaire = c.String(),
                        EtatPiece = c.String(),
                        EtatStock = c.String(),
                        DateFabrication = c.DateTime(),
                        Fabriquant = c.String(),
                        Garantissable = c.Boolean(),
                        Periode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        DepartementId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SousCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        CategorieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategorieId, cascadeDelete: true)
                .Index(t => t.CategorieId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        CategorieId = c.Int(),
                        SousCategorieId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategorieId)
                .ForeignKey("dbo.SousCategories", t => t.SousCategorieId)
                .Index(t => t.CategorieId)
                .Index(t => t.SousCategorieId);
            
            CreateTable(
                "dbo.BonSortieLignes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qnt = c.Int(),
                        ArticleId = c.Long(),
                        DepartementId = c.Int(),
                        ServiceId = c.Int(),
                        BonSortieId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.BonSorties", t => t.BonSortieId, cascadeDelete: true)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.ArticleId)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId)
                .Index(t => t.BonSortieId);
            
            CreateTable(
                "dbo.BonSorties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NBon = c.Long(),
                        DateSortie = c.DateTime(),
                        MagasinId = c.Int(),
                        CommandeInterneId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommandeInternes", t => t.CommandeInterneId)
                .ForeignKey("dbo.Magasins", t => t.MagasinId)
                .Index(t => t.MagasinId)
                .Index(t => t.CommandeInterneId);
            
            CreateTable(
                "dbo.CommandeInternes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        DateCommnde = c.DateTime(nullable: false),
                        DepartementId = c.Int(),
                        ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.CommandeLignes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommandeInterneId = c.Int(nullable: false),
                        ArticleId = c.Long(nullable: false),
                        Qnt = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.CommandeInternes", t => t.CommandeInterneId, cascadeDelete: true)
                .Index(t => t.CommandeInterneId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.PieceMagasins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(nullable: false),
                        BonEntreeId = c.Long(),
                        MagasinId = c.Int(),
                        PieceNInventaire = c.String(),
                        Date = c.DateTime(nullable: false),
                        Etat = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BonEntrees", t => t.BonEntreeId)
                .ForeignKey("dbo.Magasins", t => t.MagasinId)
                .ForeignKey("dbo.Pieces", t => t.PieceId, cascadeDelete: true)
                .Index(t => t.PieceId)
                .Index(t => t.BonEntreeId)
                .Index(t => t.MagasinId);
            
            CreateTable(
                "dbo.Reformets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(nullable: false),
                        PieceNInventaire = c.String(),
                        Date = c.DateTime(nullable: false),
                        Etat = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.PieceId, cascadeDelete: true)
                .Index(t => t.PieceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reformets", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.PieceMagasins", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.PieceMagasins", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.PieceMagasins", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.CommandeLignes", "CommandeInterneId", "dbo.CommandeInternes");
            DropForeignKey("dbo.CommandeLignes", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.BonSortieLignes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.BonSortieLignes", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.BonSortieLignes", "BonSortieId", "dbo.BonSorties");
            DropForeignKey("dbo.BonSorties", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.BonSorties", "CommandeInterneId", "dbo.CommandeInternes");
            DropForeignKey("dbo.CommandeInternes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.CommandeInternes", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.BonSortieLignes", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Types", "SousCategorieId", "dbo.SousCategories");
            DropForeignKey("dbo.Types", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.SousCategories", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.Articles", "SousCategorieId", "dbo.SousCategories");
            DropForeignKey("dbo.Articles", "MarqueId", "dbo.Marques");
            DropForeignKey("dbo.Articles", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.BonEntreeLignes", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.BonEntrees", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Magasins", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.PieceServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Employes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.PieceServices", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.Pieces", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.PieceServices", "EmployeeId", "dbo.Employes");
            DropForeignKey("dbo.Employes", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.BonEntreeLignes", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Reformets", new[] { "PieceId" });
            DropIndex("dbo.PieceMagasins", new[] { "MagasinId" });
            DropIndex("dbo.PieceMagasins", new[] { "BonEntreeId" });
            DropIndex("dbo.PieceMagasins", new[] { "PieceId" });
            DropIndex("dbo.CommandeLignes", new[] { "ArticleId" });
            DropIndex("dbo.CommandeLignes", new[] { "CommandeInterneId" });
            DropIndex("dbo.CommandeInternes", new[] { "ServiceId" });
            DropIndex("dbo.CommandeInternes", new[] { "DepartementId" });
            DropIndex("dbo.BonSorties", new[] { "CommandeInterneId" });
            DropIndex("dbo.BonSorties", new[] { "MagasinId" });
            DropIndex("dbo.BonSortieLignes", new[] { "BonSortieId" });
            DropIndex("dbo.BonSortieLignes", new[] { "ServiceId" });
            DropIndex("dbo.BonSortieLignes", new[] { "DepartementId" });
            DropIndex("dbo.BonSortieLignes", new[] { "ArticleId" });
            DropIndex("dbo.Types", new[] { "SousCategorieId" });
            DropIndex("dbo.Types", new[] { "CategorieId" });
            DropIndex("dbo.SousCategories", new[] { "CategorieId" });
            DropIndex("dbo.Services", new[] { "DepartementId" });
            DropIndex("dbo.Pieces", new[] { "ArticleId" });
            DropIndex("dbo.PieceServices", new[] { "EmployeeId" });
            DropIndex("dbo.PieceServices", new[] { "ServiceId" });
            DropIndex("dbo.PieceServices", new[] { "PieceId" });
            DropIndex("dbo.Employes", new[] { "ServiceId" });
            DropIndex("dbo.Employes", new[] { "DepartementId" });
            DropIndex("dbo.Magasins", new[] { "DepartementId" });
            DropIndex("dbo.BonEntrees", new[] { "MagasinId" });
            DropIndex("dbo.BonEntreeLignes", new[] { "BonEntreeId" });
            DropIndex("dbo.BonEntreeLignes", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "MarqueId" });
            DropIndex("dbo.Articles", new[] { "TypeId" });
            DropIndex("dbo.Articles", new[] { "SousCategorieId" });
            DropIndex("dbo.Articles", new[] { "CategorieId" });
            DropTable("dbo.Reformets");
            DropTable("dbo.PieceMagasins");
            DropTable("dbo.CommandeLignes");
            DropTable("dbo.CommandeInternes");
            DropTable("dbo.BonSorties");
            DropTable("dbo.BonSortieLignes");
            DropTable("dbo.Types");
            DropTable("dbo.SousCategories");
            DropTable("dbo.Marques");
            DropTable("dbo.Categories");
            DropTable("dbo.Services");
            DropTable("dbo.Pieces");
            DropTable("dbo.PieceServices");
            DropTable("dbo.Employes");
            DropTable("dbo.Departements");
            DropTable("dbo.Magasins");
            DropTable("dbo.BonEntrees");
            DropTable("dbo.BonEntreeLignes");
            DropTable("dbo.Articles");
        }
    }
}
