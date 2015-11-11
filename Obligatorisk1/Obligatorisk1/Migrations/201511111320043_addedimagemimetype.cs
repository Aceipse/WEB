namespace Obligatorisk1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedimagemimetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Components", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Components", "ImageMimeType");
        }
    }
}
