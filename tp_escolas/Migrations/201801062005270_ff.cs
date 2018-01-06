namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes");
            AddForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes", "InstituicaoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes");
            AddForeignKey("dbo.Actividades", "Instituicao_InstituicaoID", "dbo.Instituicoes", "InstituicaoID");
        }
    }
}
