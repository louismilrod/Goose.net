namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateOfPerformanceToSet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Setlist", "DateofPerformance", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Setlist", "DateofPerformance");
        }
    }
}
