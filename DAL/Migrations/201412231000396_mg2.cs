namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg2 : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.Types", new[] { "SousCategorieId" });
            DropIndex("dbo.Types", new[] { "CategorieId" });
            DropTable("dbo.Types");
        }
    }
}
