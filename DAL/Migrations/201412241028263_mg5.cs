namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg5 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PieceEmployees", name: "EmployeeId", newName: "Employe_Id");
            RenameIndex(table: "dbo.PieceEmployees", name: "IX_EmployeeId", newName: "IX_Employe_Id");
            CreateTable(
                "dbo.SousServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        DepartementId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departements", t => t.DepartementId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.DepartementId)
                .Index(t => t.ServiceId);
            
            AddColumn("dbo.PieceEmployees", "SousServiceId", c => c.Int());
            CreateIndex("dbo.PieceEmployees", "SousServiceId");
            AddForeignKey("dbo.PieceEmployees", "SousServiceId", "dbo.SousServices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PieceEmployees", "SousServiceId", "dbo.SousServices");
            DropForeignKey("dbo.SousServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.SousServices", "DepartementId", "dbo.Departements");
            DropIndex("dbo.SousServices", new[] { "ServiceId" });
            DropIndex("dbo.SousServices", new[] { "DepartementId" });
            DropIndex("dbo.PieceEmployees", new[] { "SousServiceId" });
            DropColumn("dbo.PieceEmployees", "SousServiceId");
            DropTable("dbo.SousServices");
            RenameIndex(table: "dbo.PieceEmployees", name: "IX_Employe_Id", newName: "IX_EmployeeId");
            RenameColumn(table: "dbo.PieceEmployees", name: "Employe_Id", newName: "EmployeeId");
        }
    }
}
