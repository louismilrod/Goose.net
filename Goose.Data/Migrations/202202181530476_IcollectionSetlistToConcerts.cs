namespace Goose.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IcollectionSetlistToConcerts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concert", "Encore_SetlistId", "dbo.Setlist");
            DropForeignKey("dbo.Concert", "Set_1_SetlistId", "dbo.Setlist");
            DropForeignKey("dbo.Concert", "Set_2_SetlistId", "dbo.Setlist");
            DropForeignKey("dbo.Concert", "Set_3_SetlistId", "dbo.Setlist");
            DropIndex("dbo.Setlist", new[] { "Concert_ConcertId" });
            DropIndex("dbo.Concert", new[] { "Encore_SetlistId" });
            DropIndex("dbo.Concert", new[] { "Set_1_SetlistId" });
            DropIndex("dbo.Concert", new[] { "Set_2_SetlistId" });
            DropIndex("dbo.Concert", new[] { "Set_3_SetlistId" });
            DropColumn("dbo.Setlist", "ConcertId");
            RenameColumn(table: "dbo.Setlist", name: "Concert_ConcertId", newName: "ConcertId");
            AlterColumn("dbo.Setlist", "ConcertId", c => c.Int());
            CreateIndex("dbo.Setlist", "ConcertId");
            DropColumn("dbo.Concert", "Set1_Id");
            DropColumn("dbo.Concert", "Set2_Id");
            DropColumn("dbo.Concert", "Set3_Id");
            DropColumn("dbo.Concert", "Encore_Id");
            DropColumn("dbo.Concert", "Encore_SetlistId");
            DropColumn("dbo.Concert", "Set_1_SetlistId");
            DropColumn("dbo.Concert", "Set_2_SetlistId");
            DropColumn("dbo.Concert", "Set_3_SetlistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Concert", "Set_3_SetlistId", c => c.Int());
            AddColumn("dbo.Concert", "Set_2_SetlistId", c => c.Int());
            AddColumn("dbo.Concert", "Set_1_SetlistId", c => c.Int());
            AddColumn("dbo.Concert", "Encore_SetlistId", c => c.Int());
            AddColumn("dbo.Concert", "Encore_Id", c => c.Int());
            AddColumn("dbo.Concert", "Set3_Id", c => c.Int());
            AddColumn("dbo.Concert", "Set2_Id", c => c.Int());
            AddColumn("dbo.Concert", "Set1_Id", c => c.Int(nullable: false));
            DropIndex("dbo.Setlist", new[] { "ConcertId" });
            AlterColumn("dbo.Setlist", "ConcertId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Setlist", name: "ConcertId", newName: "Concert_ConcertId");
            AddColumn("dbo.Setlist", "ConcertId", c => c.Int(nullable: false));
            CreateIndex("dbo.Concert", "Set_3_SetlistId");
            CreateIndex("dbo.Concert", "Set_2_SetlistId");
            CreateIndex("dbo.Concert", "Set_1_SetlistId");
            CreateIndex("dbo.Concert", "Encore_SetlistId");
            CreateIndex("dbo.Setlist", "Concert_ConcertId");
            AddForeignKey("dbo.Concert", "Set_3_SetlistId", "dbo.Setlist", "SetlistId");
            AddForeignKey("dbo.Concert", "Set_2_SetlistId", "dbo.Setlist", "SetlistId");
            AddForeignKey("dbo.Concert", "Set_1_SetlistId", "dbo.Setlist", "SetlistId");
            AddForeignKey("dbo.Concert", "Encore_SetlistId", "dbo.Setlist", "SetlistId");
        }
    }
}
