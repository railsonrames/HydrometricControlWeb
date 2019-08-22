using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hidro.Web.Models
{
    public class Consumo
    {
        public Guid Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public double ValorExcedente { get; set; }
        public int VolumeExcedente { get; set; }
        public DateTime DataRegistro { get; set; }
        public bool ExclusaoLogica { get; set; }

        public Guid IdImposto { get; set; }
        public Imposto Imposto { get; set; }

        public Guid IdCondominio { get; set; }
        public Condominio Condominio { get; set; }
    }
}
