namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcertIdImplemented : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Setlist", "ConcertId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Setlist", "ConcertId");
        }
    }
}
