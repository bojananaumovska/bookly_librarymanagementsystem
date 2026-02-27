namespace Library_Managment_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookImageUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImagePath");
        }
    }
}
