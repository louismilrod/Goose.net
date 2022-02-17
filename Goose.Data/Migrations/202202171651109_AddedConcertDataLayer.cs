namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConcertDataLayer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concert",
                c => new
                    {
                        ConcertId = c.Int(nullable: false, identity: true),
                        BandName = c.String(nullable: false),
                        Set1_Id = c.Int(nullable: false),
                        Set2_Id = c.Int(),
                        Set3_Id = c.Int(),
                        Encore_Id = c.Int(),
                        Notes = c.String(),
                        Location = c.String(nullable: false),
                        PerformanceDate = c.DateTime(nullable: false),
                        VenueName = c.String(),
                        Encore_SetlistId = c.Int(),
                        Set_1_SetlistId = c.Int(),
                        Set_2_SetlistId = c.Int(),
                        Set_3_SetlistId = c.Int(),
                    })
                .PrimaryKey(t => t.ConcertId)
                .ForeignKey("dbo.Setlist", t => t.Encore_SetlistId)
                .ForeignKey("dbo.Setlist", t => t.Set_1_SetlistId)
                .ForeignKey("dbo.Setlist", t => t.Set_2_SetlistId)
                .ForeignKey("dbo.Setlist", t => t.Set_3_SetlistId)
                .Index(t => t.Encore_SetlistId)
                .Index(t => t.Set_1_SetlistId)
                .Index(t => t.Set_2_SetlistId)
                .Index(t => t.Set_3_SetlistId);
            
            AddColumn("dbo.Setlist", "Concert_ConcertId", c => c.Int());
            CreateIndex("dbo.Setlist", "Concert_ConcertId");
            AddForeignKey("dbo.Setlist", "Concert_ConcertId", "dbo.Concert", "ConcertId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Setlist", "Concert_ConcertId", "dbo.Concert");
            DropForeignKey("dbo.Concert", "Set_3_SetlistId", "dbo.Setlist");
            DropForeignKey("dbo.Concert", "Set_2_SetlistId", "dbo.Setlist");
            DropForeignKey("dbo.Concert", "Set_1_SetlistId", "dbo.Setlist");
            DropForeignKey("dbo.Concert", "Encore_SetlistId", "dbo.Setlist");
            DropIndex("dbo.Concert", new[] { "Set_3_SetlistId" });
            DropIndex("dbo.Concert", new[] { "Set_2_SetlistId" });
            DropIndex("dbo.Concert", new[] { "Set_1_SetlistId" });
            DropIndex("dbo.Concert", new[] { "Encore_SetlistId" });
            DropIndex("dbo.Setlist", new[] { "Concert_ConcertId" });
            DropColumn("dbo.Setlist", "Concert_ConcertId");
            DropTable("dbo.Concert");
        }
    }
}
