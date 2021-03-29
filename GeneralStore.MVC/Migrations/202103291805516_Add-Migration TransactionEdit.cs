namespace GeneralStore.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMigrationTransactionEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "DateOfTransaction", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.Transactions", "DateTimeOffset");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "DateTimeOffset", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.Transactions", "DateOfTransaction");
        }
    }
}
