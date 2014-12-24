namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lf4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommandeInternes", "DateCommnde", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommandeInternes", "DateCommnde", c => c.DateTime(nullable: false));
        }
    }
}
