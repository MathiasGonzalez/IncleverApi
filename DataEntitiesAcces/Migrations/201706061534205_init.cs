namespace DataEntitiesAcces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        userid = c.Guid(nullable: false),
                        logins = c.Long(),
                        seePublic = c.Boolean(),
                        onlyPrivate = c.Boolean(),
                    })
                .PrimaryKey(t => t.userid)
                .ForeignKey("dbo.Users", t => t.userid)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userid = c.Guid(nullable: false, identity: true),
                        UID = c.String(),
                        email = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        userName = c.String(),
                        password = c.String(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userid);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryid = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.categoryid);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        type = c.String(),
                        value = c.String(),
                        isLink = c.Boolean(),
                        code = c.Int(),
                        snipettid = c.Guid(),
                        Group_groupid = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Snippets", t => t.snipettid)
                .ForeignKey("dbo.Groups", t => t.Group_groupid)
                .Index(t => t.snipettid)
                .Index(t => t.Group_groupid);
            
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        snipetid = c.Guid(nullable: false, identity: true),
                        id = c.Int(),
                        groupid = c.Int(),
                        title = c.String(),
                        description = c.String(),
                        date = c.DateTime(),
                    })
                .PrimaryKey(t => t.snipetid)
                .ForeignKey("dbo.Groups", t => t.groupid)
                .Index(t => t.groupid);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        groupid = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        tags = c.String(),
                        date = c.DateTime(),
                        isPrivate = c.Boolean(),
                        categoryid = c.Int(),
                    })
                .PrimaryKey(t => t.groupid)
                .ForeignKey("dbo.Categories", t => t.categoryid)
                .Index(t => t.categoryid);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        tag = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.tag);
            
            CreateTable(
                "dbo.GroupPermissions",
                c => new
                    {
                        groupid = c.Int(nullable: false),
                        userid = c.Guid(nullable: false),
                        favorite = c.Boolean(),
                        sticky = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.groupid, t.userid })
                .ForeignKey("dbo.Groups", t => t.groupid)
                .ForeignKey("dbo.Users", t => t.userid)
                .Index(t => t.groupid)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.TagSnippets",
                c => new
                    {
                        Tag_tag = c.String(nullable: false, maxLength: 128),
                        Snippet_snipetid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_tag, t.Snippet_snipetid })
                .ForeignKey("dbo.Tags", t => t.Tag_tag, cascadeDelete: true)
                .ForeignKey("dbo.Snippets", t => t.Snippet_snipetid, cascadeDelete: true)
                .Index(t => t.Tag_tag)
                .Index(t => t.Snippet_snipetid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupPermissions", "userid", "dbo.Users");
            DropForeignKey("dbo.GroupPermissions", "groupid", "dbo.Groups");
            DropForeignKey("dbo.TagSnippets", "Snippet_snipetid", "dbo.Snippets");
            DropForeignKey("dbo.TagSnippets", "Tag_tag", "dbo.Tags");
            DropForeignKey("dbo.Snippets", "groupid", "dbo.Groups");
            DropForeignKey("dbo.Fields", "Group_groupid", "dbo.Groups");
            DropForeignKey("dbo.Groups", "categoryid", "dbo.Categories");
            DropForeignKey("dbo.Fields", "snipettid", "dbo.Snippets");
            DropForeignKey("dbo.Accounts", "userid", "dbo.Users");
            DropIndex("dbo.TagSnippets", new[] { "Snippet_snipetid" });
            DropIndex("dbo.TagSnippets", new[] { "Tag_tag" });
            DropIndex("dbo.GroupPermissions", new[] { "userid" });
            DropIndex("dbo.GroupPermissions", new[] { "groupid" });
            DropIndex("dbo.Groups", new[] { "categoryid" });
            DropIndex("dbo.Snippets", new[] { "groupid" });
            DropIndex("dbo.Fields", new[] { "Group_groupid" });
            DropIndex("dbo.Fields", new[] { "snipettid" });
            DropIndex("dbo.Accounts", new[] { "userid" });
            DropTable("dbo.TagSnippets");
            DropTable("dbo.GroupPermissions");
            DropTable("dbo.Tags");
            DropTable("dbo.Groups");
            DropTable("dbo.Snippets");
            DropTable("dbo.Fields");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
