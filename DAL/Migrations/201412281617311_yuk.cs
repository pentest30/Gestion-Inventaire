namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yuk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistoriqueInventaires", "PieceId", "dbo.Pieces");
            DropForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees");
            DropForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins");
            DropForeignKey("dbo.HistoriqueInventaires", "ReformetId", "dbo.Reformets");
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceMagasinId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceEmployeeId" });
            DropIndex("dbo.HistoriqueInventaires", new[] { "ReformetId" });
            DropTable("dbo.HistoriqueInventaires");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HistoriqueInventaires",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PieceId = c.Long(),
                        PieceMagasinId = c.Long(),
                        PieceEmployeeId = c.Long(),
                        ReformetId = c.Long(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.HistoriqueInventaires", "ReformetId");
            CreateIndex("dbo.HistoriqueInventaires", "PieceEmployeeId");
            CreateIndex("dbo.HistoriqueInventaires", "PieceMagasinId");
            CreateIndex("dbo.HistoriqueInventaires", "PieceId");
            AddForeignKey("dbo.HistoriqueInventaires", "ReformetId", "dbo.Reformets", "Id");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceMagasinId", "dbo.PieceMagasins", "Id");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceEmployeeId", "dbo.PieceEmployees", "Id");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceId", "dbo.Pieces", "Id");
        }
    }
}
