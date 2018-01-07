namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes");
            DropIndex("dbo.Actividades", new[] { "Instituicao_InstituicaoID" });
            AlterColumn("dbo.Actividades", "Instituicao_InstituicaoID", c => c.Int(nullable: false));
            CreateIndex("dbo.Actividades", "Instituicao_InstituicaoID");
            AddForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes", "InstituicaoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes");
            DropIndex("dbo.Actividades", new[] { "Instituicao_InstituicaoID" });
            AlterColumn("dbo.Actividades", "Instituicao_InstituicaoID", c => c.Int());
            CreateIndex("dbo.Actividades", "Instituicao_InstituicaoID");
            AddForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes", "InstituicaoID");
        }
    }
}
