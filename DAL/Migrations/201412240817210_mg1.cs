namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PieceServices", newName: "PieceEmployees");
            AddColumn("dbo.Pieces", "MagasinId", c => c.Int(nullable: false));
            AddColumn("dbo.Pieces", "BonEntreeId", c => c.Long(nullable: false));
            AddColumn("dbo.Pieces", "DateEntree", c => c.DateTime());
            CreateIndex("dbo.Pieces", "MagasinId");
            CreateIndex("dbo.Pieces", "BonEntreeId");
            AddForeignKey("dbo.Pieces", "BonEntreeId", "dbo.BonEntrees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pieces", "MagasinId", "dbo.Magasins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pieces", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Pieces", "BonEntreeId", "dbo.BonEntrees");
            DropIndex("dbo.Pieces", new[] { "BonEntreeId" });
            DropIndex("dbo.Pieces", new[] { "MagasinId" });
            DropColumn("dbo.Pieces", "DateEntree");
            DropColumn("dbo.Pieces", "BonEntreeId");
            DropColumn("dbo.Pieces", "MagasinId");
            RenameTable(name: "dbo.PieceEmployees", newName: "PieceServices");
        }
    }
}
