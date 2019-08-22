using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hidro.Web.Models
{
    public class Unidade
    {
        public Guid Id { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Hidrometro { get; set; }
        public string Responsavel { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Data de Registro")]
        public DateTime DataRegistro { get; set; }
        public bool ExclusaoLogica { get; set; }

        [Display(Name = "Condomínio")]
        public Guid IdCondominio { get; set; }
        [Display(Name = "Condomínio")]
        public Condominio Condominio { get; set; }

        public IEnumerable<Leitura> Leituras { get; set; }
    }
}
