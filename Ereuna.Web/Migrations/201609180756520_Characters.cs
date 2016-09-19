namespace Ereuna.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Characters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        WhenAdded = c.DateTime(nullable: false),
                        WhenUpdated = c.DateTime(nullable: false),
                        CharacterType_Id = c.Int(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacterTypes", t => t.CharacterType_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.CharacterType_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.CharacterTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Characters", "CharacterType_Id", "dbo.CharacterTypes");
            DropIndex("dbo.Characters", new[] { "Project_Id" });
            DropIndex("dbo.Characters", new[] { "CharacterType_Id" });
            DropTable("dbo.CharacterTypes");
            DropTable("dbo.Characters");
        }
    }
}
