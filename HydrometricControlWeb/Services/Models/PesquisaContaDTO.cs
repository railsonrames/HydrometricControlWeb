using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public class PesquisaContaDTO
    {
        public Guid IdCondominio { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
