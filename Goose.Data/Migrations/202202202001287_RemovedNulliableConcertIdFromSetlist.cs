namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNulliableConcertIdFromSetlist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Setlist", "ConcertId", "dbo.Concert");
            DropIndex("dbo.Setlist", new[] { "ConcertId" });
            AlterColumn("dbo.Setlist", "ConcertId", c => c.Int(nullable: false));
            CreateIndex("dbo.Setlist", "ConcertId");
            AddForeignKey("dbo.Setlist", "ConcertId", "dbo.Concert", "ConcertId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Setlist", "ConcertId", "dbo.Concert");
            DropIndex("dbo.Setlist", new[] { "ConcertId" });
            AlterColumn("dbo.Setlist", "ConcertId", c => c.Int());
            CreateIndex("dbo.Setlist", "ConcertId");
            AddForeignKey("dbo.Setlist", "ConcertId", "dbo.Concert", "ConcertId");
        }
    }
}
