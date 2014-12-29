namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements");
            DropIndex("dbo.SousServices", new[] { "DepartementId" });
            AddColumn("dbo.HistoriqueInventaires", "PieceId", c => c.Long());
            AlterColumn("dbo.SousServices", "DepartementId", c => c.Int());
            CreateIndex("dbo.SousServices", "DepartementId");
            AddForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements");
            DropIndex("dbo.SousServices", new[] { "DepartementId" });
            AlterColumn("dbo.SousServices", "DepartementId", c => c.Int(nullable: false));
            DropColumn("dbo.HistoriqueInventaires", "PieceId");
            CreateIndex("dbo.SousServices", "DepartementId");
            AddForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements", "Id", cascadeDelete: true);
        }
    }
}
