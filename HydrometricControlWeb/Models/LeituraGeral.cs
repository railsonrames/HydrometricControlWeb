using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hidro.Web.Models
{
    public class LeituraGeral
    {
        public Guid Id { get; set; }
        [Display(Name = "M³")]
        public int MetrosCubicos { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Valor { get; set; }
        [Display(Name = "Realizado em")]
        public DateTime DataRealizacao { get; set; }
        [Display(Name = "Registrado no sistema em")]
        public DateTime DataRegistro { get; set; }
        public bool ExclusaoLogica { get; set; }

        //FK's para as tabelas abaixo
        public Guid IdCondominio { get; set; }
        public Condominio Condominio { get; set; }
    }
}
