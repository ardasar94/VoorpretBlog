namespace VoorpretBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropColumn("dbo.Likes", "UserId");
            RenameColumn(table: "dbo.Likes", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Likes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Likes", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Likes", new[] { "UserId" });
            AlterColumn("dbo.Likes", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Likes", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Likes", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Likes", "User_Id");
        }
    }
}
