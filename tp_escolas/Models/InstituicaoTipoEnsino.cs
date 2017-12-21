using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{


    [Table("InstituicoesTipoEnsino")]
    public class InstituicaoTipoEnsino
    {
        [Key, Column(Order = 0), ForeignKey("Instituicoes")]
        public int InstituicoesID { get; set; }
        [Key, Column(Order = 1), ForeignKey("TipoEnsino")]
        public int TipoEnsinoID { get; set; }
        

        public Instituicao Instituicoes { get; set; }
        public TipoEnsino TipoEnsino { get; set; } 
        public double Valor { get; set; }
    }
}