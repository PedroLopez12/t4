namespace footballapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MatchTournaments", newName: "TournamentMatches");
            DropPrimaryKey("dbo.TournamentMatches");
            AddPrimaryKey("dbo.TournamentMatches", new[] { "Tournament_Id", "Match_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TournamentMatches");
            AddPrimaryKey("dbo.TournamentMatches", new[] { "Match_Id", "Tournament_Id" });
            RenameTable(name: "dbo.TournamentMatches", newName: "MatchTournaments");
        }
    }
}
