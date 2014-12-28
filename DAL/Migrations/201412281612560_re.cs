namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class re : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceEmployees", "Utilisation", c => c.Boolean(nullable: false));
            DropColumn("dbo.PieceEmployees", "Discription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PieceEmployees", "Discription", c => c.String());
            DropColumn("dbo.PieceEmployees", "Utilisation");
        }
    }
}
