using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tp_escolas.Models.ViewModels
{
    public class ActividadeViewModelAdd
    {

        [Key]
        public int ActividadeID { get; set; }
        [MinLength(10)]
        public string Descricao { get; set; }
         

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data De Inicio")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Date de Termino")]
        [DataType(DataType.Date)]
        public DateTime DataTermino { get; set; }
    }
}