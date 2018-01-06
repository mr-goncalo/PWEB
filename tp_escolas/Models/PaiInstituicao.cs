using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{
    [Table("PaisInstituicoes")]
    public class PaiInstituicao
    {
        [Key, Column(Order = 0), ForeignKey("Pais")]
        public int PaisID { get; set; }
        [Key, Column(Order = 1), ForeignKey("Instituicoes")]
        public int InstituicoesID { get; set; }


        virtual public Pai Pais { get; set; }
        virtual public Instituicao Instituicoes { get; set; }
        public DateTime Data { get; set; }
        public bool Activo { get; set; }
    }
}