namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceMagasins", "EtatStock", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PieceMagasins", "EtatStock");
        }
    }
}
