using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tp_escolas.Models.ViewModels
{
    public class TipoEnsinoServicoViewModelAE
    {
        public int TipoEnsinoID { get; set; }
        public string Descricao { get; set; }

        public bool IsSelected { get; set; } 
        public List<Servico> Servicos { get; set; }
    }
}