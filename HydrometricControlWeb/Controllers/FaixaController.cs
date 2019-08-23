//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Hidro.Domain.Referencia.Interfaces;
//using Hidro.Web.Models.Referencia;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace Hidro.Web.Controllers.Referencia
//{
//    public class FaixaController : Controller
//    {
//        private readonly IMapper _mapper;
//        private readonly IFaixaService _faixaService;
//        private readonly IImpostoService _impostoService;

//        public FaixaController(IMapper mapper, IFaixaService faixaService, IImpostoService impostoService)
//        {
//            _mapper = mapper;
//            _faixaService = faixaService;
//            _impostoService = impostoService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            List<Faixa> faixas = _mapper.Map<List<Faixa>>(await _faixaService.Listar());
//            return View(faixas);
//        }

//        public async Task<IActionResult> Detalhar(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Faixa faixa = _mapper.Map<Faixa>(await _faixaService.Buscar(id));

//            if (faixa == null)
//                return NotFound();

//            return View(faixa);
//        }

//        public async Task<IActionResult> Criar()
//        {
//            ViewBag.IdImposto = new SelectList(await _impostoService.Listar(), "Id", "Nome");
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Criar ([Bind("Nome,Minimo,Maximo,Ordem,Aliquota,IdImposto,Ativo")] Faixa faixa)
//        {
//            if (ModelState.IsValid)
//            {
//                await _faixaService.Criar(_mapper.Map<Data.Entities.Referencia.TbFaixa>(faixa));
//                return RedirectToAction(nameof(Index));
//            }
//            // !!!
//            ViewBag.IdImposto = new SelectList(await _impostoService.Listar(), "Id", "Nome", faixa.IdImposto);
//            return View(faixa);
//        }

//        public async Task<IActionResult> Editar(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Faixa faixa = _mapper.Map<Faixa>(await _faixaService.Buscar(id));
//            ViewBag.IdImposto = new SelectList(await _impostoService.Listar(), "Id", "Nome", faixa.IdImposto);

//            if (faixa == null)
//                return NotFound();

//            return View(faixa);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Editar(Guid id, [Bind("Id,Nome,Minimo,Maximo,Ordem,Aliquota,IdImposto,DataRegistro,Ativo")] Faixa faixa)
//        {
//            if (id != faixa.Id)
//                return NotFound();

//            if (ModelState.IsValid)
//            {
//                Data.Entities.Referencia.TbFaixa tbFaixa = _mapper.Map<Data.Entities.Referencia.TbFaixa>(faixa);
//                try
//                {
//                    await _faixaService.Alterar(id, tbFaixa);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!FaixaExists(id))
//                        return NotFound();
//                    else
//                        throw;
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(faixa);
//        }

//        public async Task<IActionResult> Excluir(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Faixa faixa = _mapper.Map<Faixa>(await _faixaService.Buscar(id));

//            if (faixa == null)
//                return NotFound();

//            return View(faixa);
//        }

//        [HttpPost, ActionName("Excluir")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ExcluirConfirmed(Guid id)
//        {
//            await _faixaService.Excluir(await _faixaService.Buscar(id));
//            return RedirectToAction(nameof(Index));
//        }

//        private bool FaixaExists(Guid id)
//        {
//            if (_faixaService.Buscar(id) == null)
//                return false;
//            else
//                return true;
//        }
//    }
//}