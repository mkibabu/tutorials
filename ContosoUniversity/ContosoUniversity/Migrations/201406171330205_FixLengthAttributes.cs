namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLengthAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Instructor", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Instructor", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Instructor", "FirstName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Instructor", "LastName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 10));
        }
    }
}
