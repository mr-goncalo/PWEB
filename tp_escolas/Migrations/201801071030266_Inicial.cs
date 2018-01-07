namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actividades",
                c => new
                    {
                        ActividadeID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataTermino = c.DateTime(nullable: false),
                        Instituicao_InstituicaoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActividadeID)
                .ForeignKey("dbo.Instituicoes", t => t.Instituicao_InstituicaoID, cascadeDelete: true)
                .Index(t => t.Instituicao_InstituicaoID);
            
            CreateTable(
                "dbo.Instituicoes",
                c => new
                    {
                        InstituicaoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 120),
                        Morada = c.String(nullable: false, maxLength: 200),
                        Telefone = c.Int(nullable: false),
                        CodPostal = c.String(nullable: false),
                        Activa = c.Boolean(nullable: false),
                        UserID = c.String(),
                        TipoInstituicao = c.Int(nullable: false),
                        Cidade_CidadeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstituicaoID)
                .ForeignKey("dbo.Cidades", t => t.Cidade_CidadeID, cascadeDelete: true)
                .Index(t => t.Cidade_CidadeID);
            
            CreateTable(
                "dbo.Avaliacoes",
                c => new
                    {
                        Data = c.DateTime(nullable: false),
                        Descricao = c.String(),
                        Nota = c.Int(nullable: false),
                        Instituicoes_InstituicaoID = c.Int(nullable: false),
                        Pais_PaisID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Data)
                .ForeignKey("dbo.Instituicoes", t => t.Instituicoes_InstituicaoID, cascadeDelete: true)
                .ForeignKey("dbo.Pais", t => t.Pais_PaisID, cascadeDelete: true)
                .Index(t => t.Instituicoes_InstituicaoID)
                .Index(t => t.Pais_PaisID);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        PaisID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Telefone = c.Int(nullable: false),
                        Morada = c.String(nullable: false, maxLength: 300),
                        CodPostal = c.String(nullable: false),
                        UserID = c.String(),
                        Cidade_CidadeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaisID)
                .ForeignKey("dbo.Cidades", t => t.Cidade_CidadeID, cascadeDelete: false)
                .Index(t => t.Cidade_CidadeID);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        CidadeID = c.Int(nullable: false, identity: true),
                        CidadeNome = c.String(),
                    })
                .PrimaryKey(t => t.CidadeID);
            
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
            
            CreateTable(
                "dbo.InstituicoesServicos",
                c => new
                    {
                        InstituicoesID = c.Int(nullable: false),
                        ServicosID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstituicoesID, t.ServicosID })
                .ForeignKey("dbo.Instituicoes", t => t.InstituicoesID, cascadeDelete: true)
                .ForeignKey("dbo.Servicos", t => t.ServicosID, cascadeDelete: true)
                .Index(t => t.InstituicoesID)
                .Index(t => t.ServicosID);
            
            CreateTable(
                "dbo.Servicos",
                c => new
                    {
                        ServicosID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.ServicosID);
            
            CreateTable(
                "dbo.InstituicoesTipoEnsinoServicos",
                c => new
                    {
                        InstituicoesID = c.Int(nullable: false),
                        TipoEnsinoID = c.Int(nullable: false),
                        ServicosID = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstituicoesID, t.TipoEnsinoID, t.ServicosID })
                .ForeignKey("dbo.Instituicoes", t => t.InstituicoesID, cascadeDelete: true)
                .ForeignKey("dbo.Servicos", t => t.ServicosID, cascadeDelete: true)
                .ForeignKey("dbo.TipoEnsino", t => t.TipoEnsinoID, cascadeDelete: true)
                .Index(t => t.InstituicoesID)
                .Index(t => t.TipoEnsinoID)
                .Index(t => t.ServicosID);
            
            CreateTable(
                "dbo.TipoEnsino",
                c => new
                    {
                        TipoEnsinoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.TipoEnsinoID);
            
            CreateTable(
                "dbo.InstituicoesTipoEnsino",
                c => new
                    {
                        InstituicoesID = c.Int(nullable: false),
                        TipoEnsinoID = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstituicoesID, t.TipoEnsinoID })
                .ForeignKey("dbo.Instituicoes", t => t.InstituicoesID, cascadeDelete: true)
                .ForeignKey("dbo.TipoEnsino", t => t.TipoEnsinoID, cascadeDelete: true)
                .Index(t => t.InstituicoesID)
                .Index(t => t.TipoEnsinoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes");
            DropForeignKey("dbo.InstituicoesServicos", "ServicosID", "dbo.Servicos");
            DropForeignKey("dbo.InstituicoesTipoEnsinoServicos", "TipoEnsinoID", "dbo.TipoEnsino");
            DropForeignKey("dbo.InstituicoesTipoEnsino", "TipoEnsinoID", "dbo.TipoEnsino");
            DropForeignKey("dbo.InstituicoesTipoEnsino", "InstituicoesID", "dbo.Instituicoes");
            DropForeignKey("dbo.InstituicoesTipoEnsinoServicos", "ServicosID", "dbo.Servicos");
            DropForeignKey("dbo.InstituicoesTipoEnsinoServicos", "InstituicoesID", "dbo.Instituicoes");
            DropForeignKey("dbo.InstituicoesServicos", "InstituicoesID", "dbo.Instituicoes");
            DropForeignKey("dbo.Instituicoes", "Cidade_CidadeID", "dbo.Cidades");
            DropForeignKey("dbo.Avaliacoes", "Pais_PaisID", "dbo.Pais");
            DropForeignKey("dbo.PaisInstituicoes", "PaisID", "dbo.Pais");
            DropForeignKey("dbo.PaisInstituicoes", "InstituicoesID", "dbo.Instituicoes");
            DropForeignKey("dbo.Pais", "Cidade_CidadeID", "dbo.Cidades");
            DropForeignKey("dbo.Avaliacoes", "Instituicoes_InstituicaoID", "dbo.Instituicoes");
            DropIndex("dbo.InstituicoesTipoEnsino", new[] { "TipoEnsinoID" });
            DropIndex("dbo.InstituicoesTipoEnsino", new[] { "InstituicoesID" });
            DropIndex("dbo.InstituicoesTipoEnsinoServicos", new[] { "ServicosID" });
            DropIndex("dbo.InstituicoesTipoEnsinoServicos", new[] { "TipoEnsinoID" });
            DropIndex("dbo.InstituicoesTipoEnsinoServicos", new[] { "InstituicoesID" });
            DropIndex("dbo.InstituicoesServicos", new[] { "ServicosID" });
            DropIndex("dbo.InstituicoesServicos", new[] { "InstituicoesID" });
            DropIndex("dbo.PaisInstituicoes", new[] { "InstituicoesID" });
            DropIndex("dbo.PaisInstituicoes", new[] { "PaisID" });
            DropIndex("dbo.Pais", new[] { "Cidade_CidadeID" });
            DropIndex("dbo.Avaliacoes", new[] { "Pais_PaisID" });
            DropIndex("dbo.Avaliacoes", new[] { "Instituicoes_InstituicaoID" });
            DropIndex("dbo.Instituicoes", new[] { "Cidade_CidadeID" });
            DropIndex("dbo.Actividades", new[] { "Instituicao_InstituicaoID" });
            DropTable("dbo.InstituicoesTipoEnsino");
            DropTable("dbo.TipoEnsino");
            DropTable("dbo.InstituicoesTipoEnsinoServicos");
            DropTable("dbo.Servicos");
            DropTable("dbo.InstituicoesServicos");
            DropTable("dbo.PaisInstituicoes");
            DropTable("dbo.Cidades");
            DropTable("dbo.Pais");
            DropTable("dbo.Avaliacoes");
            DropTable("dbo.Instituicoes");
            DropTable("dbo.Actividades");
        }
    }
}
