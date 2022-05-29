namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Up", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Down", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Down");
            DropColumn("dbo.Products", "Up");
        }
    }
}
