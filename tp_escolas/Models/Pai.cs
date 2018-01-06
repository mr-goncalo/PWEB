using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations.Schema;
 using tp_escolas.ValidationAttributes;

namespace tp_escolas.Models
{

     

  [Table("Pais")]
    public class Pai
    {
        [Key]
        [ScaffoldColumn(false)]
        public int PaisID { get; set; }

        [Required]
        [MinPalavras(2, ErrorMessage = "{0} Demasiado pequeno")]  
        [Display(Name = "Nome Completo")]
        public String Nome { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{9}$")]
        [Display(Name = "Numero De Telefone")] 
        public int Telefone { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Morada { get; set; }

        [Required] 
        virtual public Cidade Cidade { get; set; }

        [Required]
        [Display(Name = "Codigo Postal")]
        [RegularExpression(@"\d{4}(-\d{3})?$" , ErrorMessage = "Codigo Postal inválido")]
        public string CodPostal { get; set; }

        [ScaffoldColumn(false)]
        public string UserID {get;set;}
          
        virtual public IList<Avaliacao> Avaliacoes { get; set; }
        virtual public IList<PaiInstituicao> PaiInstituição { get; set; }
        
    }
}