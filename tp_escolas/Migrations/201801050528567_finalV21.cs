namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalV21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actividades", "DataTermino", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Actividades", "DataTermino", c => c.DateTime());
        }
    }
}
