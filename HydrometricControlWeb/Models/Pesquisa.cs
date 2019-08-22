using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hidro.Web.Models
{
    public class Pesquisa
    {
        public Guid IdCondominio { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
