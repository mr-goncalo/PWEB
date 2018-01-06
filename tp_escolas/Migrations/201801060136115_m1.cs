namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Avaliacoes", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avaliacoes", "Data", c => c.DateTime(nullable: false));
        }
    }
}
