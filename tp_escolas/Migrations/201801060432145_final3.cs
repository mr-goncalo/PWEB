namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Avaliacoes");
            AddPrimaryKey("dbo.Avaliacoes", new[] { "PaisID", "InstituicoesID", "Data" });
            DropColumn("dbo.Avaliacoes", "AvaliacaoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Avaliacoes", "AvaliacaoID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Avaliacoes");
            AddPrimaryKey("dbo.Avaliacoes", new[] { "AvaliacaoID", "PaisID", "InstituicoesID" });
        }
    }
}
