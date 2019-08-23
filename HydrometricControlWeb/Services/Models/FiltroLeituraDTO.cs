using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public class FiltroLeituraDTO
    {
        public Guid? IdCondominio { get; set; }
        public Guid? IdUnidade { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
