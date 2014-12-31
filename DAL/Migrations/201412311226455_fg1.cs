namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PvChangemants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PiceId = c.Long(),
                        PiceIdChange = c.Long(nullable: false),
                        CodeLocation = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PvChangemants");
        }
    }
}
