namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMigrationErrors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropPrimaryKey("dbo.Course");
            DropPrimaryKey("dbo.Enrollment");
            DropColumn("dbo.Course", "ID");
            DropColumn("dbo.Enrollment", "ID");
            AddColumn("dbo.Course", "CourseID", c => c.Int(nullable: false));
            AddColumn("dbo.Enrollment", "EnrollmentID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Course", "CourseID");
            AddPrimaryKey("dbo.Enrollment", "EnrollmentID");
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollment", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Course", "ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropPrimaryKey("dbo.Enrollment");
            DropPrimaryKey("dbo.Course");
            DropColumn("dbo.Enrollment", "EnrollmentID");
            DropColumn("dbo.Course", "CourseID");
            AddPrimaryKey("dbo.Enrollment", "ID");
            AddPrimaryKey("dbo.Course", "ID");
            AddForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
