namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilhosIscritos", "FilhosID", "dbo.Filhos");
            DropForeignKey("dbo.FilhosIscritos", "InstituicoesID", "dbo.Instituicoes");
            DropForeignKey("dbo.FilhosIscritos", "TipoEnsinoID", "dbo.TipoEnsino");
            DropForeignKey("dbo.Filhos", "Pais_PaisID", "dbo.Pais");
            DropForeignKey("dbo.Avaliacoes", "FilhosID", "dbo.Filhos");
            DropIndex("dbo.Avaliacoes", new[] { "FilhosID" });
            DropIndex("dbo.Filhos", new[] { "Pais_PaisID" });
            DropIndex("dbo.FilhosIscritos", new[] { "FilhosID" });
            DropIndex("dbo.FilhosIscritos", new[] { "InstituicoesID" });
            DropIndex("dbo.FilhosIscritos", new[] { "TipoEnsinoID" });
            DropPrimaryKey("dbo.Avaliacoes");
            CreateTable(
                "dbo.PaisInstituicoes",
                c => new
                    {
                        PaisID = c.Int(nullable: false),
                        InstituicoesID = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PaisID, t.InstituicoesID })
                .ForeignKey("dbo.Instituicoes", t => t.InstituicoesID, cascadeDelete: true)
                .ForeignKey("dbo.Pais", t => t.PaisID, cascadeDelete: true)
                .Index(t => t.PaisID)
                .Index(t => t.InstituicoesID);
            
            AddPrimaryKey("dbo.Avaliacoes", new[] { "PaisID", "InstituicoesID" });
            DropColumn("dbo.Avaliacoes", "FilhosID");
            DropColumn("dbo.InstituicoesServicos", "Valor");
            DropTable("dbo.FilhosIscritos");
            DropTable("dbo.Filhos");
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilhosIscritos",
                c => new
                    {
                        FilhosID = c.Int(nullable: false),
                        InstituicoesID = c.Int(nullable: false),
                        TipoEnsinoID = c.Int(nullable: false),
                        DataInscricao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilhosID, t.InstituicoesID, t.TipoEnsinoID });
            
            CreateTable(
                "dbo.Filhos",
                c => new
                    {
                        FilhoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Pais_PaisID = c.Int(),
                    })
                .PrimaryKey(t => t.FilhoID);
            
            AddColumn("dbo.InstituicoesServicos", "Valor", c => c.Double(nullable: false));
            AddColumn("dbo.Avaliacoes", "FilhosID", c => c.Int(nullable: false));
            DropForeignKey("dbo.PaisInstituicoes", "PaisID", "dbo.Pais");
            DropForeignKey("dbo.PaisInstituicoes", "InstituicoesID", "dbo.Instituicoes");
            DropIndex("dbo.PaisInstituicoes", new[] { "InstituicoesID" });
            DropIndex("dbo.PaisInstituicoes", new[] { "PaisID" });
            DropPrimaryKey("dbo.Avaliacoes");
            DropTable("dbo.PaisInstituicoes");
            AddPrimaryKey("dbo.Avaliacoes", new[] { "PaisID", "InstituicoesID", "FilhosID" });
            CreateIndex("dbo.FilhosIscritos", "TipoEnsinoID");
            CreateIndex("dbo.FilhosIscritos", "InstituicoesID");
            CreateIndex("dbo.FilhosIscritos", "FilhosID");
            CreateIndex("dbo.Filhos", "Pais_PaisID");
            CreateIndex("dbo.Avaliacoes", "FilhosID");
            AddForeignKey("dbo.Avaliacoes", "FilhosID", "dbo.Filhos", "FilhoID", cascadeDelete: true);
            AddForeignKey("dbo.Filhos", "Pais_PaisID", "dbo.Pais", "PaisID");
            AddForeignKey("dbo.FilhosIscritos", "TipoEnsinoID", "dbo.TipoEnsino", "TipoEnsinoID", cascadeDelete: true);
            AddForeignKey("dbo.FilhosIscritos", "InstituicoesID", "dbo.Instituicoes", "InstituicaoID", cascadeDelete: true);
            AddForeignKey("dbo.FilhosIscritos", "FilhosID", "dbo.Filhos", "FilhoID", cascadeDelete: true);
        }
    }
}
