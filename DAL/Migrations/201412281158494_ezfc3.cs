namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ezfc3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoriqueInventaires", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoriqueInventaires", "Date");
        }
    }
}
