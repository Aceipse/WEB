namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanInfoToComponent2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations");
            DropIndex("dbo.Components", new[] { "LoanInformation_Id" });
            RenameColumn(table: "dbo.Components", name: "LoanInformation_Id", newName: "LoanInformationId");
            AlterColumn("dbo.Components", "LoanInformationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Components", "LoanInformationId");
            AddForeignKey("dbo.Components", "LoanInformationId", "dbo.LoanInformations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Components", "LoanInformationId", "dbo.LoanInformations");
            DropIndex("dbo.Components", new[] { "LoanInformationId" });
            AlterColumn("dbo.Components", "LoanInformationId", c => c.Int());
            RenameColumn(table: "dbo.Components", name: "LoanInformationId", newName: "LoanInformation_Id");
            CreateIndex("dbo.Components", "LoanInformation_Id");
            AddForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations", "Id");
        }
    }
}
