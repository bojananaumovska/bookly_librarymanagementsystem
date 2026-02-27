namespace Library_Managment_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Loans", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AlterColumn("dbo.Authors", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Authors", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Authors", "Bio", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Authors", "ImagePath", c => c.String(maxLength: 255));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Books", "ISBN", c => c.String(nullable: false, maxLength: 17));
            AlterColumn("dbo.Books", "Publisher", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Books", "Description", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Books", "ImagePath", c => c.String(maxLength: 255));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.BookCopies", "InventoryNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BookCopies", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Loans", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Loans", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Reservations", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Reservations", "Status", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "UserId");
            CreateIndex("dbo.Reservations", "UserId");
            AddForeignKey("dbo.Loans", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Loans", new[] { "UserId" });
            AlterColumn("dbo.Reservations", "Status", c => c.String());
            AlterColumn("dbo.Reservations", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AlterColumn("dbo.Loans", "Status", c => c.String());
            AlterColumn("dbo.Loans", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.BookCopies", "Status", c => c.String());
            AlterColumn("dbo.BookCopies", "InventoryNumber", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Books", "ImagePath", c => c.String());
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Books", "Publisher", c => c.String());
            AlterColumn("dbo.Books", "ISBN", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            AlterColumn("dbo.Authors", "ImagePath", c => c.String());
            AlterColumn("dbo.Authors", "Bio", c => c.String());
            AlterColumn("dbo.Authors", "LastName", c => c.String());
            AlterColumn("dbo.Authors", "FirstName", c => c.String());
            CreateIndex("dbo.Reservations", "UserId");
            CreateIndex("dbo.Loans", "UserId");
            AddForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Loans", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
