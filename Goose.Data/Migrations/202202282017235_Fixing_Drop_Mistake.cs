namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixing_Drop_Mistake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConcertsAttended", "InAttendance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConcertsAttended", "InAttendance");
        }
    }
}
