namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceEmployees", "DepartementId", c => c.Int(nullable: false));
            CreateIndex("dbo.PieceEmployees", "DepartementId");
            AddForeignKey("dbo.PieceEmployees", "DepartementId", "dbo.Departements", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PieceEmployees", "DepartementId", "dbo.Departements");
            DropIndex("dbo.PieceEmployees", new[] { "DepartementId" });
            DropColumn("dbo.PieceEmployees", "DepartementId");
        }
    }
}
