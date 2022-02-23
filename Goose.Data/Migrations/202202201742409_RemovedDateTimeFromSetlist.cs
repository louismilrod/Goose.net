namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDateTimeFromSetlist : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Setlist", "DateofPerformance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Setlist", "DateofPerformance", c => c.DateTime(nullable: false));
        }
    }
}
