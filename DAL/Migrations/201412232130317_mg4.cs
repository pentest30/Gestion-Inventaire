namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pieces", "CategorieId", c => c.Int());
            AddColumn("dbo.Pieces", "SousCategorieId", c => c.Int());
            AddColumn("dbo.Pieces", "TypeId", c => c.Int());
            CreateIndex("dbo.Pieces", "CategorieId");
            CreateIndex("dbo.Pieces", "SousCategorieId");
            CreateIndex("dbo.Pieces", "TypeId");
            AddForeignKey("dbo.Pieces", "CategorieId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Pieces", "SousCategorieId", "dbo.SousCategories", "Id");
            AddForeignKey("dbo.Pieces", "TypeId", "dbo.Types", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pieces", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Pieces", "SousCategorieId", "dbo.SousCategories");
            DropForeignKey("dbo.Pieces", "CategorieId", "dbo.Categories");
            DropIndex("dbo.Pieces", new[] { "TypeId" });
            DropIndex("dbo.Pieces", new[] { "SousCategorieId" });
            DropIndex("dbo.Pieces", new[] { "CategorieId" });
            DropColumn("dbo.Pieces", "TypeId");
            DropColumn("dbo.Pieces", "SousCategorieId");
            DropColumn("dbo.Pieces", "CategorieId");
        }
    }
}
