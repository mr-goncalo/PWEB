using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
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
        public DbSet<Filho> Filhos { get; set; }
        public DbSet<FilhoInscrito> FilhosInscritos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<InstituicaoServico> InstituicoesServicos { get; set; }
        public DbSet<InstituicaoTipoEnsino> InstituicaoTipoEnsino { get; set; }
        public DbSet<InstituicaoTipoEnsinoServico> InstituicoesTipoEnsinoServicos { get; set; }
        public DbSet<Pai> Pais { get; set; } 
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<TipoEnsino> TipoEnsino { get; set; }

         

    }
}