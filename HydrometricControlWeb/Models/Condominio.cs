using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hidro.Web.Models
{
    public class Condominio
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Responsavel { get; set; }
        public string Telefone { get; set; }
        public string Cnpj { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data de Registro")]
        public DateTime DataRegistro { get; set; }
        public bool ExclusaoLogica { get; set; }

        public IEnumerable<Unidade> Unidades { get; set; }
        public IEnumerable<Consumo> Consumos { get; set; }
        public IEnumerable<LeituraGeral> LeiturasGerais { get; set; }
    }
}
