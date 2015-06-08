namespace Ereuna.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Occurred = c.DateTime(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacebookUserId = c.String(),
                        LastFacebookToken = c.String(),
                        Token = c.String(),
                        First = c.String(),
                        Last = c.String(),
                        Email = c.String(),
                        IsEmailVerified = c.Boolean(nullable: false),
                        UserType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserType_Id)
                .Index(t => t.UserType_Id);
            
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionToken = c.String(),
                        SessionStarted = c.DateTime(nullable: false),
                        IsSessionOpen = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserType_Id", "dbo.UserTypes");
            DropForeignKey("dbo.UserSessions", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSessions", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "UserType_Id" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.UserSessions");
            DropTable("dbo.Users");
            DropTable("dbo.ApplicationActivities");
        }
    }
}
