namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcertsAttended : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConcertsAttended",
                c => new
                    {
                        ConcertsAttendedId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ConcertId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConcertsAttendedId)
                .ForeignKey("dbo.Concert", t => t.ConcertId, cascadeDelete: true)
                .Index(t => t.ConcertId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConcertsAttended", "ConcertId", "dbo.Concert");
            DropIndex("dbo.ConcertsAttended", new[] { "ConcertId" });
            DropTable("dbo.ConcertsAttended");
        }
    }
}
