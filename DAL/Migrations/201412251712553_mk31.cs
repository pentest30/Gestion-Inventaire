namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mk31 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PieceMagasins", new[] { "BonEntree_Id" });
            DropColumn("dbo.PieceMagasins", "BonEntreeId");
            RenameColumn(table: "dbo.PieceMagasins", name: "BonEntree_Id", newName: "BonEntreeId");
            AlterColumn("dbo.PieceMagasins", "BonEntreeId", c => c.Long());
            CreateIndex("dbo.PieceMagasins", "BonEntreeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PieceMagasins", new[] { "BonEntreeId" });
            AlterColumn("dbo.PieceMagasins", "BonEntreeId", c => c.Int());
            RenameColumn(table: "dbo.PieceMagasins", name: "BonEntreeId", newName: "BonEntree_Id");
            AddColumn("dbo.PieceMagasins", "BonEntreeId", c => c.Int());
            CreateIndex("dbo.PieceMagasins", "BonEntree_Id");
        }
    }
}
