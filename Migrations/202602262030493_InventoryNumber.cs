namespace Library_Managment_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BookCopies", "InventoryNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookCopies", "InventoryNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
