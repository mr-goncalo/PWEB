﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tp_escolas.ValidationAttributes;


namespace tp_escolas.Models.ViewModels
{
    public class InstituicaoViewModelAdd
    {
        [NotMapped]
        [Required]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Insira um email válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [NotMapped]
        [StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [MinPalavras(2, ErrorMessage = "Nome demasiado pequeno! ")]
        [MaxLength(120)]
        [Display(Name = "Nome da Instituicao")]
        public string Nome { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Morada demasiado pequena!")]
        public string Morada { get; set; }

        [Required]
        public Cidade Cidade { get; set; }

        [Required]
        [Display(Name = "Numero de Telefone")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Numero de Telefone Incorrecto!")]
        public int Telefone { get; set; }

        [Required]
        [Display(Name = "Codigo Postal")]
        [RegularExpression(@"\d{4}(-\d{3})?$", ErrorMessage = "Codigo Postal inválido")]
        public string CodPostal { get; set; }

        [Required]
        public TipoInstituicao TipoInstituicao { get; set; }
         
        public List<Cidade> Cidades { get; set; }
        public List<Servico> Servicos { get; set; }
        public List<TipoEnsino> TiposEnsino { get; set; }

    }
}