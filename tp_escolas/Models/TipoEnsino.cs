using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{

    [Table("TipoEnsino")] 
    public class TipoEnsino
    {
        [Key]
        public int TipoEnsinoID { get; set; }
        public string Descricao { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public double Valor { get; set; }

        public IList<InstituicaoTipoEnsinoServico> InstituicoesTipoEnsinoServicos { get; set; }
        public IList<InstituicaoTipoEnsino> InstituicoesTipoEnsino { get; set; }
        public IList<FilhoInscrito> FilhosInscritos { get; set; }

    }
}