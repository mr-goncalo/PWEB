using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{
    [Table("FilhosIscritos")]
    public class FilhoInscrito
    { 

        [Key, Column(Order = 0), ForeignKey("Filhos")]
        public int FilhosID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Instituicoes")]
        public int InstituicoesID { get; set; }
        [Key, Column(Order = 2), ForeignKey("TipoEnsino")]
        public int TipoEnsinoID { get; set; }
         
        public Filho Filhos { get; set; }
        public Instituicao Instituicoes { get; set; } 
        public TipoEnsino TipoEnsino { get; set; }
         
        public DateTime DataInscricao { get; set; } 
    }
}