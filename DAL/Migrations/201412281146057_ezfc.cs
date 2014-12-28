namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ezfc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees");
            DropForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins");
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceMagasinId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceEmployeeId" });
            AlterColumn("dbo.HistoriqueInventaires", "PieceMagasinId", c => c.Long());
            AlterColumn("dbo.HistoriqueInventaires", "PieceEmployeeId", c => c.Long());
            AlterColumn("dbo.HistoriqueInventaires", "RefrometId", c => c.Long());
            CreateIndex("dbo.HistoriqueInventaires", "PieceMagasinId");
            CreateIndex("dbo.HistoriqueInventaires", "PieceEmployeeId");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees", "Id");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins");
            DropForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees");
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceEmployeeId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceMagasinId" });
            AlterColumn("dbo.HistoriqueInventaires", "RefrometId", c => c.Long(nullable: false));
            AlterColumn("dbo.HistoriqueInventaires", "PieceEmployeeId", c => c.Long(nullable: false));
            AlterColumn("dbo.HistoriqueInventaires", "PieceMagasinId", c => c.Long(nullable: false));
            CreateIndex("dbo.HistoriqueInventaires", "PieceEmployeeId");
            CreateIndex("dbo.HistoriqueInventaires", "PieceMagasinId");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees", "Id", cascadeDelete: true);
        }
    }
}
