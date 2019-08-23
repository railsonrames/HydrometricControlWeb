//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Hidro.Domain.Habitacao.Interfaces;
//using Hidro.Domain.Interfaces;
//using Hidro.Web.Models.Habitacao;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace Hidro.Web.Controllers.Habitacao
//{
//    public class UnidadeController : Controller
//    {
//        private readonly IMapper _mapper;
//        private readonly IUnidadeService _unidadeService;
//        private readonly ICondominioService _condominioService;

//        public UnidadeController(IMapper mapper, IUnidadeService unidadeService, ICondominioService condominioService)
//        {
//            _mapper = mapper;
//            _unidadeService = unidadeService;
//            _condominioService = condominioService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            List<Unidade> unidades = _mapper.Map<List<Unidade>>(await _unidadeService.Listar());
//            return View(unidades);
//        }

//        public async Task<IActionResult> Detalhar(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Unidade unidade = _mapper.Map<Unidade>(await _unidadeService.Buscar(id));

//            if (unidade == null)
//                return NotFound();

//            return View(unidade);
//        }

//        public async Task<IActionResult> Criar()
//        {
//            ViewBag.IdCondominio = new SelectList(await _condominioService.Listar(), "Id", "Nome");
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Criar([Bind("Numero,Hidrometro,Responsavel,Cpf,Telefone,Email,Ativo,IdCondominio")] Unidade unidade)
//        {
//            if (ModelState.IsValid)
//            {
//                await _unidadeService.Criar(_mapper.Map<Data.Entities.Habitacao.TbUnidade>(unidade));
//                return RedirectToAction(nameof(Index));
//            }
//            ViewBag.IdCondominio = new SelectList(await _condominioService.Listar(), "Id", "Nome", unidade.IdCondominio);
//            return View(unidade);
//        }

//        public async Task<IActionResult> Editar(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Unidade unidade = _mapper.Map<Unidade>(await _unidadeService.Buscar(id));
//            ViewBag.IdCondominio = new SelectList(await _condominioService.Listar(), "Id", "Nome", unidade.IdCondominio);

//            if (unidade == null)
//                return NotFound();

//            return View(unidade);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Editar(Guid id, [Bind("Id,Numero,Hidrometro,Responsavel,Cpf,Telefone,Email,Ativo,DataRegistro,IdCondominio")] Unidade unidade)
//        {
//            if (id != unidade.Id)
//                return NotFound();

//            if (ModelState.IsValid)
//            {
//                Data.Entities.Habitacao.TbUnidade tbUnidade = _mapper.Map<Data.Entities.Habitacao.TbUnidade>(unidade);
//                try
//                {
//                    await _unidadeService.Alterar(id, tbUnidade);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UnidadeExists(id))
//                        return NotFound();
//                    else
//                        throw;
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(unidade);
//        }

//        public async Task<IActionResult> Excluir(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Unidade unidade = _mapper.Map<Unidade>(await _unidadeService.Buscar(id));

//            if (unidade == null)
//                return NotFound();

//            return View(unidade);
//        }

//        [HttpPost, ActionName("Excluir")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ExcluirConfirmed(Guid id)
//        {
//            await _unidadeService.Excluir(await _unidadeService.Buscar(id));
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UnidadeExists(Guid id)
//        {
//            if (_unidadeService.Buscar(id) == null)
//                return false;
//            else
//                return true;
//        }
        
//        public async Task<JsonResult> ListarPorCondominio(Guid idCondominio)
//        {
//            var Unidades = new SelectList(await _unidadeService.ListarPorCondominio(idCondominio), "Id", "Numero");
//            return Json(Unidades);
//        }
//    }
//}