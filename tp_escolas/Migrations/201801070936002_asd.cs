namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Avaliacoes", "InstituicoesID", "dbo.Instituicoes");
            DropIndex("dbo.Avaliacoes", new[] { "InstituicoesID" });
            AddColumn("dbo.Avaliacoes", "Instituicoes_InstituicaoID", c => c.Int());
            AddColumn("dbo.Avaliacoes", "Instituicao_InstituicaoID", c => c.Int());
            CreateIndex("dbo.Avaliacoes", "Instituicoes_InstituicaoID");
            CreateIndex("dbo.Avaliacoes", "Instituicao_InstituicaoID");
            AddForeignKey("dbo.Avaliacoes", "Instituicao_InstituicaoID", "dbo.Instituicoes", "InstituicaoID");
            AddForeignKey("dbo.Avaliacoes", "Instituicoes_InstituicaoID", "dbo.Instituicoes", "InstituicaoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avaliacoes", "Instituicoes_InstituicaoID", "dbo.Instituicoes");
            DropForeignKey("dbo.Avaliacoes", "Instituicao_InstituicaoID", "dbo.Instituicoes");
            DropIndex("dbo.Avaliacoes", new[] { "Instituicao_InstituicaoID" });
            DropIndex("dbo.Avaliacoes", new[] { "Instituicoes_InstituicaoID" });
            DropColumn("dbo.Avaliacoes", "Instituicao_InstituicaoID");
            DropColumn("dbo.Avaliacoes", "Instituicoes_InstituicaoID");
            CreateIndex("dbo.Avaliacoes", "InstituicoesID");
            AddForeignKey("dbo.Avaliacoes", "InstituicoesID", "dbo.Instituicoes", "InstituicaoID", cascadeDelete: true);
        }
    }
}
