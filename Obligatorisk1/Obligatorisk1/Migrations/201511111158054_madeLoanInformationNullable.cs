namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeLoanInformationNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoanInformations", "LoanDate", c => c.DateTime());
            AlterColumn("dbo.LoanInformations", "ReturnDate", c => c.DateTime());
            AlterColumn("dbo.LoanInformations", "ReservationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoanInformations", "ReservationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LoanInformations", "ReturnDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LoanInformations", "LoanDate", c => c.DateTime(nullable: false));
        }
    }
}
