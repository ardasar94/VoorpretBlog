namespace VoorpretBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "CreationDate");
        }
    }
}
