using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tp_escolas.Models
{
    [Table("Actividades")]
    public class Actividade
    {
        [Key]
        public int ActividadeID { get; set; }
        public string Descricao { get; set; } 
        public Instituicao Instituicao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
          

    }
}