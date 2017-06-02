namespace DataEntitiesAcces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieGuid = c.Guid(nullable: false, identity: true),
                        uuid = c.String(),
                        id = c.Int(),
                        vote_average = c.Int(),
                        vote_count = c.Int(),
                        release_date = c.String(),
                        release = c.DateTime(),
                        seen = c.Boolean(),
                        rating = c.Int(),
                        director = c.String(),
                    })
                .PrimaryKey(t => t.MovieGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false, identity: true),
                        UID = c.String(),
                        email = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        userName = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.VisualSections",
                c => new
                    {
                        section_key = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        content = c.String(),
                        VisualItemGuid = c.Guid(),
                    })
                .PrimaryKey(t => t.section_key)
                .ForeignKey("dbo.Movies", t => t.VisualItemGuid)
                .Index(t => t.VisualItemGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisualSections", "VisualItemGuid", "dbo.Movies");
            DropIndex("dbo.VisualSections", new[] { "VisualItemGuid" });
            DropTable("dbo.VisualSections");
            DropTable("dbo.Users");
            DropTable("dbo.Movies");
            DropTable("dbo.Genres");
        }
    }
}
