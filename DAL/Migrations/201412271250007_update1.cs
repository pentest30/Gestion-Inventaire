namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
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
                "dbo.PieceEmployees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(),
                        PieceNInventaire = c.String(),
                        Date = c.DateTime(nullable: false),
                        ServiceId = c.Int(),
                        SousServiceId = c.Int(),
                        Discription = c.String(),
                        Etat = c.String(),
                        Employe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.PieceId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .ForeignKey("dbo.SousServices", t => t.SousServiceId)
                .ForeignKey("dbo.Employes", t => t.Employe_Id)
                .Index(t => t.PieceId)
                .Index(t => t.ServiceId)
                .Index(t => t.SousServiceId)
                .Index(t => t.Employe_Id);
            
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
                "dbo.SousServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        DepartementId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId);
            
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
                "dbo.HistoriqueInventaires",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceMagasinId = c.Long(nullable: false),
                        PieceEmployeeId = c.Long(nullable: false),
                        RefrometId = c.Long(nullable: false),
                        Reformet_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PieceEmployees", t => t.PieceEmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.PieceMagasins", t => t.PieceMagasinId, cascadeDelete: true)
                .ForeignKey("dbo.Reformets", t => t.Reformet_Id)
                .Index(t => t.PieceMagasinId)
                .Index(t => t.PieceEmployeeId)
                .Index(t => t.Reformet_Id);
            
            CreateTable(
                "dbo.PieceMagasins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(),
                        MagasinId = c.Int(),
                        BonEntreeId = c.Long(),
                        ArticleId = c.Long(),
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
                        PieceNInventaire = c.String(),
                        Date = c.DateTime(nullable: false),
                        Etat = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.PieceId)
                .Index(t => t.PieceId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Types", "SousCategorieId", "dbo.SousCategories");
            DropForeignKey("dbo.Types", "CategorieId", "dbo.Categories");
            DropForeignKey("dbo.HistoriqueInventaires", "Reformet_Id", "dbo.Reformets");
            DropForeignKey("dbo.Reformets", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins");
            DropForeignKey("dbo.PieceMagasins", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.PieceMagasins", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.PieceMagasins", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.PieceMagasins", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees");
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
            DropForeignKey("dbo.Magasins", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.PieceEmployees", "Employe_Id", "dbo.Employes");
            DropForeignKey("dbo.PieceEmployees", "SousServiceId", "dbo.SousServices");
            DropForeignKey("dbo.SousServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.PieceEmployees", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Employes", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.PieceEmployees", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.Pieces", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Pieces", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.Pieces", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Employes", "DepartementId", "dbo.Departements");
            DropForeignKey("dbo.BonEntreeLignes", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Types", new[] { "SousCategorieId" });
            DropIndex("dbo.Types", new[] { "CategorieId" });
            DropIndex("dbo.Reformets", new[] { "PieceId" });
            DropIndex("dbo.PieceMagasins", new[] { "ArticleId" });
            DropIndex("dbo.PieceMagasins", new[] { "BonEntreeId" });
            DropIndex("dbo.PieceMagasins", new[] { "MagasinId" });
            DropIndex("dbo.PieceMagasins", new[] { "PieceId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "Reformet_Id" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceEmployeeId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceMagasinId" });
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
            DropIndex("dbo.SousServices", new[] { "ServiceId" });
            DropIndex("dbo.SousServices", new[] { "DepartementId" });
            DropIndex("dbo.Services", new[] { "DepartementId" });
            DropIndex("dbo.Pieces", new[] { "BonEntreeId" });
            DropIndex("dbo.Pieces", new[] { "MagasinId" });
            DropIndex("dbo.Pieces", new[] { "ArticleId" });
            DropIndex("dbo.PieceEmployees", new[] { "Employe_Id" });
            DropIndex("dbo.PieceEmployees", new[] { "SousServiceId" });
            DropIndex("dbo.PieceEmployees", new[] { "ServiceId" });
            DropIndex("dbo.PieceEmployees", new[] { "PieceId" });
            DropIndex("dbo.Employes", new[] { "ServiceId" });
            DropIndex("dbo.Employes", new[] { "DepartementId" });
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
            DropTable("dbo.CommandeLignes");
            DropTable("dbo.CommandeInternes");
            DropTable("dbo.BonSorties");
            DropTable("dbo.BonSortieLignes");
            DropTable("dbo.SousCategories");
            DropTable("dbo.Marques");
            DropTable("dbo.Categories");
            DropTable("dbo.SousServices");
            DropTable("dbo.Services");
            DropTable("dbo.Pieces");
            DropTable("dbo.PieceEmployees");
            DropTable("dbo.Employes");
            DropTable("dbo.Departements");
            DropTable("dbo.Magasins");
            DropTable("dbo.BonEntrees");
            DropTable("dbo.BonEntreeLignes");
            DropTable("dbo.Articles");
        }
    }
}
