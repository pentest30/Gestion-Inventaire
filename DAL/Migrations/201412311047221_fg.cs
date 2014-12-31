namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HistoriqueInventaires", "Etat", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoriqueInventaires", "Etat");
        }
    }
}
