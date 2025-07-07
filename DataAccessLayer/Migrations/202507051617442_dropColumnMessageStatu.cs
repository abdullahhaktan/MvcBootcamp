namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropColumnMessageStatu : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "MessageState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "MessageState", c => c.String());
        }
    }
}
