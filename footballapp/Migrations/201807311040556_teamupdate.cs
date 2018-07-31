namespace footballapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Teams", new[] { "Tournament_Id" });
            RenameColumn(table: "dbo.Teams", name: "Tournament_Id", newName: "tournamentId");
            AlterColumn("dbo.Teams", "tournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Teams", "tournamentId");
            AddForeignKey("dbo.Teams", "tournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "tournamentId", "dbo.Tournaments");
            DropIndex("dbo.Teams", new[] { "tournamentId" });
            AlterColumn("dbo.Teams", "tournamentId", c => c.Int());
            RenameColumn(table: "dbo.Teams", name: "tournamentId", newName: "Tournament_Id");
            CreateIndex("dbo.Teams", "Tournament_Id");
            AddForeignKey("dbo.Teams", "Tournament_Id", "dbo.Tournaments", "Id");
        }
    }
}
