namespace footballapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "Team_1_Score", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "Team_2_Score", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "Winner", c => c.String(maxLength: 50));
            AddColumn("dbo.Teams", "Points", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "Points");
            DropColumn("dbo.Matches", "Winner");
            DropColumn("dbo.Matches", "Team_2_Score");
            DropColumn("dbo.Matches", "Team_1_Score");
        }
    }
}
