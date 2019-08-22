using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hidro.Domain.Interfaces;
using Hidro.Domain.Referencia.Interfaces;
using Hidro.Web.Models.Referencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hidro.Web.Controllers.Referencia
{
    public class ConsumoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICondominioService _condominioService;
        private readonly ILeituraGeralService _leituraGeralService;
        public ConsumoController(
            IMapper mapper,
            ICondominioService condominioService,
            ILeituraGeralService leituraGeralService
            )
        {
            _mapper = mapper;
            _condominioService = condominioService;
            _leituraGeralService = leituraGeralService;
        }
        public async Task<IActionResult> Index(Guid idCondominio)
        {
            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");
            List<LeituraGeral> leiturasGerais = _mapper.Map<List<LeituraGeral>>(await _leituraGeralService.Listar(idCondominio));
            return View(leiturasGerais);
        }

        public async Task<IActionResult> Criar()
        {
            ViewBag.IdCondominio = new SelectList(await _condominioService.Listar(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("DataRealizacao,Valor,DataRegistro,ExclusaoLogica,IdCondominio,MetrosCubicos")] LeituraGeral leituraGeral)
        {
            if (ModelState.IsValid)
            {
                await _leituraGeralService.Criar(_mapper.Map<Data.Entities.Referencia.TbLeituraGeral>(leituraGeral));
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");

            return View(leituraGeral);
        }
       
        public async Task<IActionResult> Detalhar(Guid? id) {

            if (id == null)
                return NotFound();

            LeituraGeral leituraGeral = _mapper.Map<LeituraGeral>(await _leituraGeralService.Buscar(id));

            if (leituraGeral == null)
                return NotFound();

            return View(leituraGeral);

        }

        public async Task<IActionResult> Editar(Guid? id)
        {
            if (id == null)
                return NotFound();

            LeituraGeral leituraGeral = _mapper.Map<LeituraGeral>(await _leituraGeralService.Buscar(id));
            ViewBag.IdCondominio = new SelectList(await _condominioService.Listar(), "Id", "Nome", leituraGeral.IdCondominio);

            if (leituraGeral == null)
                return NotFound();

            return View(leituraGeral);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar (Guid id, [Bind("DataRealizacao,Valor,DataRegistro,ExclusaoLogica,IdCondominio,MetrosCubicos")] LeituraGeral leituraGeral)
        {
            if (id != leituraGeral.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                Data.Entities.Referencia.TbLeituraGeral tbLeituraGeral = _mapper.Map<Data.Entities.Referencia.TbLeituraGeral>(leituraGeral);
                try
                {
                    await _leituraGeralService.Editar(tbLeituraGeral);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeituraGeralExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leituraGeral);
        }

        public async Task<IActionResult> Excluir(Guid? id)
        {
            if (id == null)
                return NotFound();

            LeituraGeral leituraGeral = _mapper.Map<LeituraGeral>(await _leituraGeralService.Buscar(id));

            if (leituraGeral == null)
                return NotFound();

            return View(leituraGeral);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmed(Guid id)
        {
            await _leituraGeralService.Excluir(await _leituraGeralService.Buscar(id));
            return RedirectToAction(nameof(Index));
        }

        private bool LeituraGeralExists(Guid id)
            => _leituraGeralService.Buscar(id) == null ? false : true;
    }
}