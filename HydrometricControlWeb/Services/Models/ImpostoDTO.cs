using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public class ImpostoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data de Registro")]
        public DateTime DataRegistro { get; set; }
        public bool ExclusaoLogica { get; set; }

        public IEnumerable<FaixaDTO> Faixas { get; set; }
        public IEnumerable<ConsumoDTO> Consumos { get; set; }
        public IEnumerable<LeituraDTO> Leituras { get; set; }
    }
}
