namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Avaliacoes");
            AlterColumn("dbo.Avaliacoes", "AvaliacaoID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Avaliacoes", new[] { "AvaliacaoID", "PaisID", "InstituicoesID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Avaliacoes");
            AlterColumn("dbo.Avaliacoes", "AvaliacaoID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Avaliacoes", new[] { "AvaliacaoID", "PaisID", "InstituicoesID" });
        }
    }
}
