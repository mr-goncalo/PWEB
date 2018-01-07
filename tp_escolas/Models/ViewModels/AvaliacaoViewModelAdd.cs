using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tp_escolas.Models.ViewModels
{
    public class AvaliacaoViewModelAdd
    {
        [Required]
        virtual public int InstituicoesID { get; set; }

        public string Descricao { get; set; }
        [Range(1, 10, ErrorMessage = "Por favor classifique entre 1 e 10")]
        public int Nota { get; set; }
        
    }
}