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
        public Cidade Cidade { get; set; }
        [Required]
        [Display(Name = "Codigo Postal")]
        [RegularExpression(@"\d{4}(-\d{3})?$" , ErrorMessage = "Codigo Postal inválido")]
        public string CodPostal { get; set; }

        [ScaffoldColumn(false)]
        public string UserID {get;set;}

        [NotMapped]
        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [NotMapped]
        [Required]
        [StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

         
        [NotMapped]
        public List<Cidade> Cidades { get; set; }

        public IList<Filho> Filhos { get; set; }
        public IList<Avaliacao> Avaliacoes { get; set; }

        
    }
}