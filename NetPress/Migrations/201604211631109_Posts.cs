namespace NetPress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Posts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        postID = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        content = c.String(),
                        category = c.String(nullable: false),
                        status = c.Int(nullable: false),
                        dateCreated = c.DateTime(),
                        lastModified = c.DateTime(),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.postID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
        }
    }
}
