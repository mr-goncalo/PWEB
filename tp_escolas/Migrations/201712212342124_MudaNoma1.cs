namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudaNoma1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pais", name: "CidadeId_CidadeID", newName: "Cidades_CidadeID");
            RenameIndex(table: "dbo.Pais", name: "IX_CidadeId_CidadeID", newName: "IX_Cidades_CidadeID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pais", name: "IX_Cidades_CidadeID", newName: "IX_CidadeId_CidadeID");
            RenameColumn(table: "dbo.Pais", name: "Cidades_CidadeID", newName: "CidadeId_CidadeID");
        }
    }
}
