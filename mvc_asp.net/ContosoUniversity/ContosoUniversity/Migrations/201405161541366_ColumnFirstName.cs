namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnFirstName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Student", name: "FirstMidName", newName: "Firstname");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Student", name: "Firstname", newName: "FirstMidName");
        }
    }
}
