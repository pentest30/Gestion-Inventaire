namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Articles", "TypeId");
            AddForeignKey("dbo.Articles", "TypeId", "dbo.Types", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "TypeId", "dbo.Types");
            DropIndex("dbo.Articles", new[] { "TypeId" });
        }
    }
}
