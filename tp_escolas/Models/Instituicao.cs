using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tp_escolas.ValidationAttributes;

namespace tp_escolas.Models
{
    [Table("Instituicoes")]
    public class Instituicao  
    {
       
        [Key]
        [ScaffoldColumn(false)]
        public int InstituicaoID { get; set; }

        [Required]
        [MinPalavras(2, ErrorMessage = "Nome demasiado pequeno! ")]
        [MaxLength(120)]
        [Display(Name = "Nome da Instituicao")]
        public string Nome { get; set; }

        [Required]
        [StringLength(200,MinimumLength = 10, ErrorMessage = "Morada demasiado pequena!")]
        public string Morada { get; set; }

        [Required]
        virtual public Cidade Cidade { get; set; }

        [Required]
        [Display(Name = "Numero de Telefone")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Numero de Telefone Incorrecto!")]
        public int Telefone { get; set; }

        [Required]
        [Display(Name = "Codigo Postal")]
        [RegularExpression(@"\d{4}(-\d{3})?$", ErrorMessage = "Codigo Postal inválido")]
        public string CodPostal { get; set; }

        [ScaffoldColumn(false)]
        public bool Activa { get; set; }
        [ScaffoldColumn(false)]
        public string UserID { get; set; }
        
        [Required]
        public TipoInstituicao TipoInstituicao { get; set; }


        virtual public IList<Actividade> Actividades { get; set; }
        virtual public IList<InstituicaoServico> InstituicoesServicos { get; set; }
        virtual public IList<InstituicaoTipoEnsinoServico> InstituicoesTipoEnsinoServicos { get; set; }
        virtual public IList<InstituicaoTipoEnsino> InstituicoesTipoEnsino { get; set; }
        virtual public IList<Avaliacao> Avaliacoes { get; set; }

    }
}