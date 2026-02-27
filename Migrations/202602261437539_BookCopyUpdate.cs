namespace Library_Managment_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookCopyUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookCopies", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookCopies", "Status", c => c.Int(nullable: false));
        }
    }
}
