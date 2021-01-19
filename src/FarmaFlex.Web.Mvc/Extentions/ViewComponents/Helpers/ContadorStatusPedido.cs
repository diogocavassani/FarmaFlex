using APIFarmaFlex.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaFlex.Web.Mvc.Extentions.ViewComponents.Helpers
{
    public class ContadorStatusPedido
    {

        public int TotalStatus { get; set; }
        public string ClassContent { get; set; }
        public string LinkDetalhes { get; set; }
        public StatusEnumPedido Status { get; set; }
    }
}
