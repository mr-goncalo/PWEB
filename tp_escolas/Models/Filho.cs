using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp_escolas.Models
{
    [Table("Filhos")]
    public class Filho
    {
        [Key]
        public int FilhoID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Pai Pais { get; set; }

        public IList<Avaliacao> Avaliacoes { get; set; }
        public IList<FilhoInscrito> FilhosInscritos { get; set; }
    }

}