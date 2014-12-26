namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fh5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "TypeId", "dbo.Types");
            DropIndex("dbo.Articles", new[] { "TypeId" });
            DropColumn("dbo.Articles", "TypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "TypeId", c => c.Int());
            CreateIndex("dbo.Articles", "TypeId");
            AddForeignKey("dbo.Articles", "TypeId", "dbo.Types", "Id");
        }
    }
}
