using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{
    [Table("Servicos")] 
    public class Servico
    {
        [Key]
        public int ServicosID { get; set; }
        public string Descricao { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        virtual public IList<InstituicaoServico> InstituicoesServicos { get; set; }
        virtual public IList<InstituicaoTipoEnsinoServico> InstituicoesTipoEnsinoServicos { get; set; }
    }
}