namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avaliacoes", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avaliacoes", "Data");
        }
    }
}
