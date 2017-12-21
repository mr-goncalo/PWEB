using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using tp_escolas.Models; 
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{
    [Table("Cidades")]
    public class Cidade
    {
        [Key]
        public int CidadeID { get; set; }
        public string CidadeNome { get; set; }
         
    }
}