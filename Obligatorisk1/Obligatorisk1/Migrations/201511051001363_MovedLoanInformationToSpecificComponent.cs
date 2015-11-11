namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedLoanInformationToSpecificComponent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations");
            DropIndex("dbo.Components", new[] { "LoanInformation_Id" });
            AddColumn("dbo.SpecificComponents", "LoanInformation_Id", c => c.Int());
            CreateIndex("dbo.SpecificComponents", "LoanInformation_Id");
            AddForeignKey("dbo.SpecificComponents", "LoanInformation_Id", "dbo.LoanInformations", "Id");
            DropColumn("dbo.Components", "LoanInformation_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Components", "LoanInformation_Id", c => c.Int());
            DropForeignKey("dbo.SpecificComponents", "LoanInformation_Id", "dbo.LoanInformations");
            DropIndex("dbo.SpecificComponents", new[] { "LoanInformation_Id" });
            DropColumn("dbo.SpecificComponents", "LoanInformation_Id");
            CreateIndex("dbo.Components", "LoanInformation_Id");
            AddForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations", "Id");
        }
    }
}
