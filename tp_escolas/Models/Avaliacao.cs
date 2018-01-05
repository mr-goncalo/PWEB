using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tp_escolas.Models
{
    [Table("Avaliacoes")]
    public class Avaliacao
    { 
        [Key, Column(Order = 0), ForeignKey("Pais")]
        public int PaisID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Instituicoes")]
        public int InstituicoesID { get; set; }
      

        public Pai Pais { get; set; }
        public Instituicao Instituicoes { get; set; }
        public string Descricao { get; set; }

        public int nota { get; set; }
        public DateTime Data { get; set; }
    }
}