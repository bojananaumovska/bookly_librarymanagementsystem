namespace Library_Managment_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Authors", "ImagePath");
        }
    }
}
