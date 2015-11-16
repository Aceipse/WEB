namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequiredFields1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SpecificComponents", "SerieNr", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpecificComponents", "SerieNr", c => c.String());
        }
    }
}
