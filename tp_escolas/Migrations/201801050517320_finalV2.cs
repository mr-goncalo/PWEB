namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actividades", "Descricao", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actividades", "Descricao", c => c.String());
        }
    }
}
