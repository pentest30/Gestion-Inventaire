namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PieceServices", newName: "PieceEmployees");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PieceEmployees", newName: "PieceServices");
        }
    }
}
