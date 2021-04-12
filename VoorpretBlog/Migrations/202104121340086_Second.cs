namespace VoorpretBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropColumn("dbo.Comments", "AuthorId");
            RenameColumn(table: "dbo.Comments", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Comments", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            AlterColumn("dbo.Comments", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Comments", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Comments", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Author_Id");
        }
    }
}
