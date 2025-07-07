namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class writerEntityUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Writers", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Writers", "ImageUrl");
        }
    }
}
