namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Magasins", "Code", c => c.Guid(nullable: false));
            AlterColumn("dbo.SousServices", "Code", c => c.Guid(nullable: false));
            AlterColumn("dbo.HistoriqueInventaires", "CodeLocation", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HistoriqueInventaires", "CodeLocation", c => c.String(maxLength: 4000));
            AlterColumn("dbo.SousServices", "Code", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Magasins", "Code", c => c.String(maxLength: 4000));
        }
    }
}
