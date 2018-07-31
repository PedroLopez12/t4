namespace footballapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        matchWeek = c.Int(nullable: false),
                        Team_1 = c.String(maxLength: 50),
                        Team_2 = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MatchTournaments",
                c => new
                    {
                        Match_Id = c.Int(nullable: false),
                        Tournament_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Match_Id, t.Tournament_Id })
                .ForeignKey("dbo.Matches", t => t.Match_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id, cascadeDelete: true)
                .Index(t => t.Match_Id)
                .Index(t => t.Tournament_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchTournaments", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.MatchTournaments", "Match_Id", "dbo.Matches");
            DropIndex("dbo.MatchTournaments", new[] { "Tournament_Id" });
            DropIndex("dbo.MatchTournaments", new[] { "Match_Id" });
            DropTable("dbo.MatchTournaments");
            DropTable("dbo.Matches");
        }
    }
}
