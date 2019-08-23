//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Hidro.Domain.Conta.Interfaces;
//using Hidro.Domain.Interfaces;
//using Hidro.Web.Models.Conta;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace Hidro.Web.Controllers.Conta
//{
//    public class ContaController : Controller
//    {
//        private readonly ICondominioService _condominioService;
//        private readonly IContaService _contaService;

//        public ContaController(ICondominioService condominioService, IContaService contaService)
//        {
//            _condominioService = condominioService;
//            _contaService = contaService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");
//            return View();
//        }

//        public async Task<IActionResult> GerarPorCondominio(Pesquisa pesquisa)
//        {
//            if (ModelState.IsValid)
//            {
//                var teste = await _contaService.Buscar(pesquisa.IdCondominio, pesquisa.DataInicio, pesquisa.DataFim);
//                return View("Molde", teste);
//            }
//            else
//            {
//                return NotFound();
//            }
//        }
//    }
//}