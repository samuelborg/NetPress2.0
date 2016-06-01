namespace NetPress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamedmodels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Posts", newName: "PostModels");
            AlterColumn("dbo.PostModels", "dateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PostModels", "lastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "lastModified", c => c.DateTime());
            AlterColumn("dbo.PostModels", "dateCreated", c => c.DateTime());
            RenameTable(name: "dbo.PostModels", newName: "Posts");
        }
    }
}
