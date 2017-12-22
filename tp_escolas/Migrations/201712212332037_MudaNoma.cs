namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudaNoma : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pais", name: "Cidade_CidadeID", newName: "CidadeId_CidadeID");
            RenameIndex(table: "dbo.Pais", name: "IX_Cidade_CidadeID", newName: "IX_CidadeId_CidadeID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pais", name: "IX_CidadeId_CidadeID", newName: "IX_Cidade_CidadeID");
            RenameColumn(table: "dbo.Pais", name: "CidadeId_CidadeID", newName: "Cidade_CidadeID");
        }
    }
}
