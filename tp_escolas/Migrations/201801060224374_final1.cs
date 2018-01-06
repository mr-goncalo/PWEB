namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Avaliacoes");
            AddColumn("dbo.Avaliacoes", "AvaliacaoID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Avaliacoes", new[] { "AvaliacaoID", "PaisID", "InstituicoesID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Avaliacoes");
            DropColumn("dbo.Avaliacoes", "AvaliacaoID");
            AddPrimaryKey("dbo.Avaliacoes", new[] { "PaisID", "InstituicoesID" });
        }
    }
}
