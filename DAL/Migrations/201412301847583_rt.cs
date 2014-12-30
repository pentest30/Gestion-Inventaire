namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                        CategorieId = c.Int(),
                        SousCategorieId = c.Int(),
                        MarqueId = c.Int(),
                        Image = c.Binary(maxLength: 4000),
                        Discription = c.String(maxLength: 4000),
                        QntTotal = c.Long(),
                        QntMagsin = c.Long(),
                        QntMin = c.Int(),
                        Poids = c.Decimal(precision: 18, scale: 2),
                        Code = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategorieId)
                .ForeignKey("dbo.Marques", t => t.MarqueId)
                .ForeignKey("dbo.SousCategories", t => t.SousCategorieId)
                .Index(t => t.CategorieId)
                .Index(t => t.SousCategorieId)
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
                        Libelle = c.String(maxLength: 4000),
                        DepartementId = c.Int(),
                        Code = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.Departements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                        DepartementId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.SousServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Libelle = c.String(maxLength: 4000),
                        DepartementId = c.Int(),
                        Code = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.DepartementId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marques",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SousCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                        CategorieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategorieId, cascadeDelete: true)
                .Index(t => t.CategorieId);
            
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
                        Libelle = c.String(maxLength: 4000),
                        DateCommnde = c.DateTime(),
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
                "dbo.Employes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(maxLength: 4000),
                        Prenom = c.String(maxLength: 4000),
                        DateNaissance = c.DateTime(),
                        Fonction = c.String(maxLength: 4000),
                        DepartementId = c.Int(),
                        ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.PieceEmployees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(),
                        BonSortieId = c.Long(),
                        Date = c.DateTime(nullable: false),
                        DepartementId = c.Int(nullable: false),
                        ServiceId = c.Int(),
                        SousServiceId = c.Int(),
                        Utilisation = c.Boolean(nullable: false),
                        Etat = c.String(maxLength: 4000),
                        Employe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BonSorties", t => t.BonSortieId)
                .ForeignKey("dbo.Departements", t => t.DepartementId, cascadeDelete: true)
                .ForeignKey("dbo.Pieces", t => t.PieceId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .ForeignKey("dbo.SousServices", t => t.SousServiceId)
                .ForeignKey("dbo.Employes", t => t.Employe_Id)
                .Index(t => t.PieceId)
                .Index(t => t.BonSortieId)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId)
                .Index(t => t.SousServiceId)
                .Index(t => t.Employe_Id);
            
            CreateTable(
                "dbo.Pieces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArticleId = c.Long(nullable: false),
                        NInventaire = c.String(maxLength: 4000),
                        EtatPiece = c.String(maxLength: 4000),
                        EtatStock = c.String(maxLength: 4000),
                        DateFabrication = c.DateTime(),
                        Fabriquant = c.String(maxLength: 4000),
                        Garantissable = c.Boolean(),
                        Periode = c.Int(nullable: false),
                        MagasinId = c.Int(nullable: false),
                        BonEntreeId = c.Long(nullable: false),
                        DateEntree = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.BonEntrees", t => t.BonEntreeId, cascadeDelete: true)
                .ForeignKey("dbo.Magasins", t => t.MagasinId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.MagasinId)
                .Index(t => t.BonEntreeId);
            
            CreateTable(
                "dbo.HistoriqueInventaires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeLocation = c.Guid(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Inventaire = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PieceMagasins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(),
                        MagasinId = c.Int(),
                        BonEntreeId = c.Long(),
                        ArticleId = c.Long(),
                        Disponibilite = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.BonEntrees", t => t.BonEntreeId)
                .ForeignKey("dbo.Magasins", t => t.MagasinId)
                .ForeignKey("dbo.Pieces", t => t.PieceId)
                .Index(t => t.PieceId)
                .Index(t => t.MagasinId)
                .Index(t => t.BonEntreeId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Reformets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(),
                        PieceNInventaire = c.String(maxLength: 4000),
                        Date = c.DateTime(nullable: false),
                        Etat = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.PieceId)
                .Index(t => t.PieceId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(maxLength: 4000),
                        CategorieId = c.Int(),
                        SousCategorieId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategorieId)
                .ForeignKey("dbo.SousCategories", t => t.SousCategorieId)
                .Index(t => t.CategorieId)
                .Index(t => t.SousCategorieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Types", "SousCategorieId", "dbo.SousCategories");
            DropForeignKey("dbo.Types", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.Reformets", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.PieceMagasins", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.PieceMagasins", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.PieceMagasins", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.PieceMagasins", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Employes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.PieceEmployees", "Employe_Id", "dbo.Employes");
            DropForeignKey("dbo.PieceEmployees", "SousServiceId", "dbo.SousServices");
            DropForeignKey("dbo.PieceEmployees", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.PieceEmployees", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.Pieces", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Pieces", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.Pieces", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.PieceEmployees", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.PieceEmployees", "BonSortieId", "dbo.BonSorties");
            DropForeignKey("dbo.Employes", "DepartementId", "dbo.Departements");
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
            DropForeignKey("dbo.SousCategories", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.Articles", "SousCategorieId", "dbo.SousCategories");
            DropForeignKey("dbo.Articles", "MarqueId", "dbo.Marques");
            DropForeignKey("dbo.Articles", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.BonEntreeLignes", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.BonEntrees", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.SousServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.Services", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.Magasins", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.BonEntreeLignes", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Types", new[] { "SousCategorieId" });
            DropIndex("dbo.Types", new[] { "CategorieId" });
            DropIndex("dbo.Reformets", new[] { "PieceId" });
            DropIndex("dbo.PieceMagasins", new[] { "ArticleId" });
            DropIndex("dbo.PieceMagasins", new[] { "BonEntreeId" });
            DropIndex("dbo.PieceMagasins", new[] { "MagasinId" });
            DropIndex("dbo.PieceMagasins", new[] { "PieceId" });
            DropIndex("dbo.Pieces", new[] { "BonEntreeId" });
            DropIndex("dbo.Pieces", new[] { "MagasinId" });
            DropIndex("dbo.Pieces", new[] { "ArticleId" });
            DropIndex("dbo.PieceEmployees", new[] { "Employe_Id" });
            DropIndex("dbo.PieceEmployees", new[] { "SousServiceId" });
            DropIndex("dbo.PieceEmployees", new[] { "ServiceId" });
            DropIndex("dbo.PieceEmployees", new[] { "DepartementId" });
            DropIndex("dbo.PieceEmployees", new[] { "BonSortieId" });
            DropIndex("dbo.PieceEmployees", new[] { "PieceId" });
            DropIndex("dbo.Employes", new[] { "ServiceId" });
            DropIndex("dbo.Employes", new[] { "DepartementId" });
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
            DropIndex("dbo.SousCategories", new[] { "CategorieId" });
            DropIndex("dbo.SousServices", new[] { "DepartementId" });
            DropIndex("dbo.SousServices", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "DepartementId" });
            DropIndex("dbo.Magasins", new[] { "DepartementId" });
            DropIndex("dbo.BonEntrees", new[] { "MagasinId" });
            DropIndex("dbo.BonEntreeLignes", new[] { "BonEntreeId" });
            DropIndex("dbo.BonEntreeLignes", new[] { "ArticleId" });
            DropIndex("dbo.Articles", new[] { "MarqueId" });
            DropIndex("dbo.Articles", new[] { "SousCategorieId" });
            DropIndex("dbo.Articles", new[] { "CategorieId" });
            DropTable("dbo.Types");
            DropTable("dbo.Reformets");
            DropTable("dbo.PieceMagasins");
            DropTable("dbo.HistoriqueInventaires");
            DropTable("dbo.Pieces");
            DropTable("dbo.PieceEmployees");
            DropTable("dbo.Employes");
            DropTable("dbo.CommandeLignes");
            DropTable("dbo.CommandeInternes");
            DropTable("dbo.BonSorties");
            DropTable("dbo.BonSortieLignes");
            DropTable("dbo.SousCategories");
            DropTable("dbo.Marques");
            DropTable("dbo.Categories");
            DropTable("dbo.SousServices");
            DropTable("dbo.Services");
            DropTable("dbo.Departements");
            DropTable("dbo.Magasins");
            DropTable("dbo.BonEntrees");
            DropTable("dbo.BonEntreeLignes");
            DropTable("dbo.Articles");
        }
    }
}
