using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hidro.Domain.Referencia.Interfaces;
using Hidro.Web.Models.Referencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hidro.Web.Controllers.Referencia
{
    public class ImpostoController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImpostoService _impostoService;

        public ImpostoController(IMapper mapper, IImpostoService impostoService)
        {
            _mapper = mapper;
            _impostoService = impostoService;
        }

        public async Task<IActionResult> Index()
        {
            List<Imposto> impostos = _mapper.Map<List<Imposto>>(await _impostoService.Listar());
            return View(impostos);
        }

        public async Task<IActionResult> Detalhar(Guid? id)
        {
            if (id == null)
                return NotFound();

            Imposto imposto = _mapper.Map<Imposto>(await _impostoService.Buscar(id));

            if (imposto == null)
                return NotFound();

            return View(imposto);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Nome,Ativo")] Imposto imposto)
        {
            if (ModelState.IsValid)
            {
                await _impostoService.Criar(_mapper.Map<Data.Entities.Referencia.TbImposto>(imposto));
                return RedirectToAction(nameof(Index));
            }
            return View(imposto);
        }

        public async Task<IActionResult> Editar(Guid? id)
        {
            if (id == null)
                return NotFound();

            Imposto imposto = _mapper.Map<Imposto>(await _impostoService.Buscar(id));

            if (imposto == null)
                return NotFound();

            return View(imposto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, [Bind("Id,Nome,Ativo,DataRegistro")] Imposto imposto)
        {
            if (id != imposto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                Data.Entities.Referencia.TbImposto tbImposto = _mapper.Map<Data.Entities.Referencia.TbImposto>(imposto);
                try
                {
                    await _impostoService.Alterar(id, tbImposto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImpostoExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imposto);
        }

        public async Task<IActionResult> Excluir(Guid? id)
        {
            if (id == null)
                return NotFound();

            Imposto imposto = _mapper.Map<Imposto>(await _impostoService.Buscar(id));

            if (imposto == null)
                return NotFound();

            return View(imposto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmed(Guid id)
        {
            await _impostoService.Excluir(await _impostoService.Buscar(id));
            return RedirectToAction(nameof(Index));
        }

        private bool ImpostoExists(Guid id)
        {
            if (_impostoService.Buscar(id) == null)
                return false;
            else
                return true;
        }
    }
}