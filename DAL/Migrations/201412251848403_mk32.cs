namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mk32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceMagasins", "ArticleId", c => c.Long());
            CreateIndex("dbo.PieceMagasins", "ArticleId");
            AddForeignKey("dbo.PieceMagasins", "ArticleId", "dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PieceMagasins", "ArticleId", "dbo.Articles");
            DropIndex("dbo.PieceMagasins", new[] { "ArticleId" });
            DropColumn("dbo.PieceMagasins", "ArticleId");
        }
    }
}
