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
        [Required]
        [MinLength(10)]
        public string Descricao { get; set; } 

        public Instituicao Instituicao { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data De Inicio")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        [Required]
        [DisplayFormat( ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date de Termino")]
        [DataType(DataType.Date)]
        public DateTime  DataTermino { get; set; }
          

    }
}