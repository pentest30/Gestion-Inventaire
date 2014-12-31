namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceMagasins", "Inventaire", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PieceMagasins", "Inventaire");
        }
    }
}
