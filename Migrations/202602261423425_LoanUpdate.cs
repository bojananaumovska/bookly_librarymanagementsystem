namespace Library_Managment_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoanUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loans", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Loans", "Status", c => c.String(nullable: false));
        }
    }
}
