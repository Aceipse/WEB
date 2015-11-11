namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedloantinformation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Components", "LoanInformationId", "dbo.LoanInformations");
            DropIndex("dbo.Components", new[] { "LoanInformationId" });
            RenameColumn(table: "dbo.Components", name: "LoanInformationId", newName: "LoanInformation_Id");
            AlterColumn("dbo.Components", "LoanInformation_Id", c => c.Int());
            CreateIndex("dbo.Components", "LoanInformation_Id");
            AddForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations");
            DropIndex("dbo.Components", new[] { "LoanInformation_Id" });
            AlterColumn("dbo.Components", "LoanInformation_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Components", name: "LoanInformation_Id", newName: "LoanInformationId");
            CreateIndex("dbo.Components", "LoanInformationId");
            AddForeignKey("dbo.Components", "LoanInformationId", "dbo.LoanInformations", "Id", cascadeDelete: true);
        }
    }
}
