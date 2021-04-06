namespace VoorpretBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropColumn("dbo.Posts", "AuthorId");
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Posts", "AuthorId");
            AddForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Posts", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "Author_Id");
            AddForeignKey("dbo.Posts", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
