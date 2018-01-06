namespace tp_escolas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalV23 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pais", name: "Cidades_CidadeID", newName: "Cidade_CidadeID");
            RenameIndex(table: "dbo.Pais", name: "IX_Cidades_CidadeID", newName: "IX_Cidade_CidadeID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pais", name: "IX_Cidade_CidadeID", newName: "IX_Cidades_CidadeID");
            RenameColumn(table: "dbo.Pais", name: "Cidade_CidadeID", newName: "Cidades_CidadeID");
        }
    }
}
