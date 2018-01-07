using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
namespace tp_escolas.Models
{
    public class EscolaContext : DbContext 
    {
        public EscolaContext() : base("name=dbconn")
        {
           
        }

        public DbSet<Actividade> Actividades { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; } 
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<InstituicaoServico> InstituicoesServicos { get; set; }
        public DbSet<InstituicaoTipoEnsino> InstituicaoTipoEnsino { get; set; }
        public DbSet<InstituicaoTipoEnsinoServico> InstituicoesTipoEnsinoServicos { get; set; }
        public DbSet<Pai> Pais { get; set; } 
        public DbSet<PaiInstituicao> PaisInstituiçoes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoEnsino> TipoEnsino { get; set; }


      
    }
}