using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tp_escolas.Models
{
    [Table("InstituicoesServicos")]
    public class InstituicaoServico
    {
        [Key, Column(Order = 0), ForeignKey("Instituicoes")]
        public int InstituicoesID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Servicos")]
        public int ServicosID { get; set; }

        public Instituicao Instituicoes { get; set; }
        public Servico Servicos { get; set; }
        public double Valor { get; set; }

    }
}