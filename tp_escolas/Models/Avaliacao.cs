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
       
        [Key, Column(Order = 1), ForeignKey("Pais")]
        public int PaisID { get; set; }
        [Key, Column(Order = 2), ForeignKey("Instituicoes")] 
        public int InstituicoesID { get; set; }
        
        [Required]
        virtual public Pai Pais { get; set; }
        [Required]
        virtual public Instituicao Instituicoes { get; set; }

        public string Descricao { get; set; }
        [Range(1,10, ErrorMessage = "Por favor classifique entre 1 e 10")]
        public int Nota { get; set; }
        [ScaffoldColumn(false)]
        [Key,Column(Order =3)]
        public DateTime Data { get; set; }
    }
}