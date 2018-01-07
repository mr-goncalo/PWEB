namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vasa2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Avaliacoes", name: "Instituicoes_InstituicaoID", newName: "InstituicoesID");
            RenameColumn(table: "dbo.Avaliacoes", name: "Pais_PaisID", newName: "PaisID");
            RenameIndex(table: "dbo.Avaliacoes", name: "IX_Pais_PaisID", newName: "IX_PaisID");
            RenameIndex(table: "dbo.Avaliacoes", name: "IX_Instituicoes_InstituicaoID", newName: "IX_InstituicoesID");
            DropPrimaryKey("dbo.Avaliacoes");
            AddPrimaryKey("dbo.Avaliacoes", new[] { "PaisID", "InstituicoesID", "Data" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Avaliacoes");
            AddPrimaryKey("dbo.Avaliacoes", "Data");
            RenameIndex(table: "dbo.Avaliacoes", name: "IX_InstituicoesID", newName: "IX_Instituicoes_InstituicaoID");
            RenameIndex(table: "dbo.Avaliacoes", name: "IX_PaisID", newName: "IX_Pais_PaisID");
            RenameColumn(table: "dbo.Avaliacoes", name: "PaisID", newName: "Pais_PaisID");
            RenameColumn(table: "dbo.Avaliacoes", name: "InstituicoesID", newName: "Instituicoes_InstituicaoID");
        }
    }
}
