using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hidro.Web.Models
{
    public class Imposto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data de Registro")]
        public DateTime DataRegistro { get; set; }
        public bool ExclusaoLogica { get; set; }

        public IEnumerable<Faixa> Faixas { get; set; }
        public IEnumerable<Consumo> Consumos { get; set; }
        public IEnumerable<Leitura> Leituras { get; set; }
    }
}
