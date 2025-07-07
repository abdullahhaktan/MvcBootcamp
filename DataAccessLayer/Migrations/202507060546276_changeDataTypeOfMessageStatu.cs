namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDataTypeOfMessageStatu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "MessageStatu", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "MessageStatu", c => c.Boolean(nullable: false));
        }
    }
}
