namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migDeleteColumnMessageDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "MessageDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "MessageDate", c => c.DateTime(nullable: false));
        }
    }
}
