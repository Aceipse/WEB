namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanInfoToComponent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoanInformations", "Component_Id", "dbo.Components");
            DropIndex("dbo.LoanInformations", new[] { "Component_Id" });
            AddColumn("dbo.Components", "LoanInformation_Id", c => c.Int());
            CreateIndex("dbo.Components", "LoanInformation_Id");
            AddForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations", "Id");
            DropColumn("dbo.LoanInformations", "Component_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanInformations", "Component_Id", c => c.Int());
            DropForeignKey("dbo.Components", "LoanInformation_Id", "dbo.LoanInformations");
            DropIndex("dbo.Components", new[] { "LoanInformation_Id" });
            DropColumn("dbo.Components", "LoanInformation_Id");
            CreateIndex("dbo.LoanInformations", "Component_Id");
            AddForeignKey("dbo.LoanInformations", "Component_Id", "dbo.Components", "Id");
        }
    }
}
