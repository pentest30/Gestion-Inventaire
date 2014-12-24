namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "TypeId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "TypeId");
        }
    }
}
