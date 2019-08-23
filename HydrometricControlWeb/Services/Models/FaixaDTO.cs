using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HydrometricControlWeb.Services.Models
{
    public class FaixaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Mínimo")]
        public int Minimo { get; set; }
        [Display(Name = "Máximo")]
        public int Maximo { get; set; }
        public int Ordem { get; set; }
        [Display(Name = "Alíquota")]
        public double Aliquota { get; set; }
        [Display(Name = "Data de Registro")]
        public DateTime DataRegistro { get; set; }
        public bool Ativo { get; set; }
        public bool ExclusaoLogica { get; set; }
        [Display(Name = "Imposto")]
        public Guid IdImposto { get; set; }
        public ImpostoDTO Imposto { get; set; }
    }
}
