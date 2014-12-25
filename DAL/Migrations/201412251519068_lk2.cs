namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lk2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PieceMagasins", "BonEntreeId", "dbo.BonEntrees");
            DropIndex("dbo.PieceMagasins", new[] { "BonEntreeId" });
            DropColumn("dbo.PieceMagasins", "BonEntreeId");
            DropColumn("dbo.PieceMagasins", "PieceNInventaire");
            DropColumn("dbo.PieceMagasins", "Etat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PieceMagasins", "Etat", c => c.String());
            AddColumn("dbo.PieceMagasins", "PieceNInventaire", c => c.String());
            AddColumn("dbo.PieceMagasins", "BonEntreeId", c => c.Long());
            CreateIndex("dbo.PieceMagasins", "BonEntreeId");
            AddForeignKey("dbo.PieceMagasins", "BonEntreeId", "dbo.BonEntrees", "Id");
        }
    }
}
