using AutoMapper;
using Hidro.Domain.Interfaces;
using Hidro.Domain.Services;
using Hidro.Web.Models.Habitacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hidro.Web.Controllers.Habitacao
{
    public class CondominioController : Controller
    {
        private readonly ICondominioService _condominioService;

        public CondominioController(ICondominioService condominioService)
        {
            _condominioService = condominioService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<Condominio> listaCondominios = _mapper.Map<List<Condominio>>(await _condominioService.Listar());
            return View(listaCondominios);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Condominio condominio = _mapper.Map<Condominio>(await _condominioService.Buscar(id));

            if (condominio == null)
            {
                return NotFound();
            }

            return View(condominio);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condominio"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Endereco,Cep,Responsavel,Telefone,Cnpj,Ativo,ExclusaoLogica")] Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                Data.Entities.Habitacao.TbCondominio tbCondominio = _mapper.Map<Data.Entities.Habitacao.TbCondominio>(condominio);
                await _condominioService.Criar(tbCondominio);
                return RedirectToAction(nameof(Index));
            }
            return View(condominio);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condominio condominio = _mapper.Map<Condominio>(await _condominioService.Buscar(id));           

            if (condominio == null)
            {
                return NotFound();
            }

            return View(condominio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Endereco,Cep,Responsavel,Telefone,Cnpj,Ativo,DataRegistro,ExclusaoLogica")] Condominio condominio)
        {
            if (id != condominio.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                Data.Entities.Habitacao.TbCondominio tbCondominio = _mapper.Map<Data.Entities.Habitacao.TbCondominio>(condominio);

                try
                {
                    await _condominioService.Alterar(id, tbCondominio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CondominioExists(id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(condominio);
        }


        // GET: Condominio/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Condominio condominio = _mapper.Map<Condominio>(await _condominioService.Buscar(id));

            if (condominio == null)
            {
                return NotFound();
            }

            return View(condominio);
        }

        // POST: Condominio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tbCondominio = await _condominioService.Buscar(id);
            await _condominioService.Excluir(tbCondominio);
            return RedirectToAction(nameof(Index));
        }

        private bool CondominioExists(Guid id)
        {            
            if (_condominioService.Buscar(id) == null)
                return false;
            else
                return true;
        }
    }
}
