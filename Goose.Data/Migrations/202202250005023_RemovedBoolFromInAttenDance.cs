namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBoolFromInAttenDance : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConcertsAttended", "InAttendance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConcertsAttended", "InAttendance", c => c.Boolean(nullable: false));
        }
    }
}
