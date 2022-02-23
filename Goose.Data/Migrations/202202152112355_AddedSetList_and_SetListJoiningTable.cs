namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSetList_and_SetListJoiningTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Setlist",
                c => new
                    {
                        SetlistId = c.Int(nullable: false, identity: true),
                        SetNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SetlistId);
            
            CreateTable(
                "dbo.SongsJoinSetlist",
                c => new
                    {
                        SongsJoinSetlistId = c.Int(nullable: false, identity: true),
                        PositionInSet = c.Int(nullable: false),
                        SongId = c.Int(nullable: false),
                        SetlistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongsJoinSetlistId)
                .ForeignKey("dbo.Setlist", t => t.SetlistId, cascadeDelete: true)
                .ForeignKey("dbo.Song", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.SetlistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongsJoinSetlist", "SongId", "dbo.Song");
            DropForeignKey("dbo.SongsJoinSetlist", "SetlistId", "dbo.Setlist");
            DropIndex("dbo.SongsJoinSetlist", new[] { "SetlistId" });
            DropIndex("dbo.SongsJoinSetlist", new[] { "SongId" });
            DropTable("dbo.SongsJoinSetlist");
            DropTable("dbo.Setlist");
        }
    }
}
