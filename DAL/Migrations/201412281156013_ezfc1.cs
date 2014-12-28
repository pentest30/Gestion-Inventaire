namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ezfc1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoriqueInventaires", "PieceId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoriqueInventaires", "PieceId");
        }
    }
}
