namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mk3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceMagasins", "BonEntreeId", c => c.Int());
            AddColumn("dbo.PieceMagasins", "BonEntree_Id", c => c.Long());
            CreateIndex("dbo.PieceMagasins", "BonEntree_Id");
            AddForeignKey("dbo.PieceMagasins", "BonEntree_Id", "dbo.BonEntrees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PieceMagasins", "BonEntree_Id", "dbo.BonEntrees");
            DropIndex("dbo.PieceMagasins", new[] { "BonEntree_Id" });
            DropColumn("dbo.PieceMagasins", "BonEntree_Id");
            DropColumn("dbo.PieceMagasins", "BonEntreeId");
        }
    }
}
