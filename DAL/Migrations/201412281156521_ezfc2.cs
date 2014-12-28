namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ezfc2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.HistoriqueInventaires", name: "Reformet_Id", newName: "ReformetId");
            RenameIndex(table: "dbo.HistoriqueInventaires", name: "IX_Reformet_Id", newName: "IX_ReformetId");
            CreateIndex("dbo.HistoriqueInventaires", "PieceId");
            AddForeignKey("dbo.HistoriqueInventaires", "PieceId", "dbo.Pieces", "Id");
            DropColumn("dbo.HistoriqueInventaires", "RefrometId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HistoriqueInventaires", "RefrometId", c => c.Long());
            DropForeignKey("dbo.HistoriqueInventaires", "PieceId", "dbo.Pieces");
            DropIndex("dbo.HistoriqueInventaires", new[] { "PieceId" });
            RenameIndex(table: "dbo.HistoriqueInventaires", name: "IX_ReformetId", newName: "IX_Reformet_Id");
            RenameColumn(table: "dbo.HistoriqueInventaires", name: "ReformetId", newName: "Reformet_Id");
        }
    }
}
