namespace DataEntitiesAcces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class session : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        userid = c.Guid(nullable: false),
                        fechasession = c.DateTime(),
                        token = c.String(),
                        platform = c.String(),
                        ip = c.String(),
                    })
                .PrimaryKey(t => t.userid)
                .ForeignKey("dbo.Users", t => t.userid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "userid", "dbo.Users");
            DropIndex("dbo.Sessions", new[] { "userid" });
            DropTable("dbo.Sessions");
        }
    }
}
