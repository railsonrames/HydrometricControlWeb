//using System;
//using System.Linq;
//using AutoMapper;
//using Hidro.Data.Models;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Hidro.Domain.Afericao.Interfaces;
//using Hidro.Domain.Habitacao.Interfaces;
//using Hidro.Domain.Referencia.Interfaces;
//using Hidro.Domain.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Hidro.Web.Models.Afericao;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.IO;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Hosting;
//using System.Text;
//using NPOI.SS.UserModel;
//using NPOI.HSSF.UserModel;
//using NPOI.XSSF.UserModel;

//namespace Hidro.Web.Controllers.Afericao
//{
//    public class LeituraController : Controller
//    {
//        private readonly IMapper _mapper;
//        private readonly ILeituraService _leituraService;
//        private readonly IImpostoService _impostoService;
//        private readonly IUnidadeService _unidadeService;
//        private readonly ICondominioService _condominioService;
//        private readonly IHostingEnvironment _hostingEnvironment;

//        public LeituraController(
//            IMapper mapper,
//            ILeituraService leituraService,
//            IImpostoService impostoService,
//            IUnidadeService unidadeService,
//            ICondominioService condominioService,
//            IHostingEnvironment hostingEnvironment
//            )
//        {
//            _mapper = mapper;
//            _leituraService = leituraService;
//            _impostoService = impostoService;
//            _unidadeService = unidadeService;
//            _condominioService = condominioService;
//            _hostingEnvironment = hostingEnvironment;
//        }

//        public async Task<IActionResult> Index(FiltroLeituraWeb filtro)
//        {
//            List<Leitura> leituras = _mapper.Map<List<Leitura>>(await _leituraService.Listar());
//            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");
//            if (filtro.IdCondominio != null || filtro.IdUnidade != null || filtro.DataInicio != null || filtro.DataFim != null)
//                leituras = _mapper.Map<List<Leitura>>(await _leituraService.ListaFiltrada(_mapper.Map<FiltroLeituraData>(filtro)));

//            return View(leituras);
//        }

//        [HttpPost]
//        public async Task<IActionResult> FiltroPesquisa(FiltroLeituraWeb filtro)
//        {
//            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");
//            var lista = _mapper.Map<List<Leitura>>(await _leituraService.ListaFiltrada(_mapper.Map<FiltroLeituraData>(filtro)));
//            return View(lista);
//        }

//        public async Task<IActionResult> Detalhar(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Leitura leitura = _mapper.Map<Leitura>(await _leituraService.Buscar(id));

//            if (leitura == null)
//                return NotFound();

//            return View(leitura);
//        }

//        public async Task<IActionResult> Criar()
//        {
//            ViewBag.Impostos = new SelectList(await _impostoService.Listar(), "Id", "Nome");
//            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Criar([Bind("RealizadaEm,Valor,NomeDaImagem,Observacao,DataRegistro,ExclusaoLogica,IdUnidade,IdImposto")] Leitura leitura)
//        {
//            if (ModelState.IsValid)
//            {
//                await _leituraService.Criar(_mapper.Map<Data.Entities.Afericao.TbLeitura>(leitura));
//                return RedirectToAction(nameof(Index));
//            }
//            // !!!
//            ViewBag.IdImposto = new SelectList(await _impostoService.Listar(), "Id", "Nome");
//            ViewBag.Condominios = new SelectList(await _condominioService.Listar(), "Id", "Nome");
//            return View(leitura);
//        }

//        public async Task<IActionResult> Editar(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Leitura leitura = _mapper.Map<Leitura>(await _leituraService.Buscar(id));
//            ViewBag.IdImposto = new SelectList(await _impostoService.Listar(), "Id", "Nome");

//            if (leitura == null)
//                return NotFound();

//            return View(leitura);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Editar(Guid id, [Bind("Id,RealizadaEm,Valor,NomeDaImagem,Observacao,DataRegistro,ExclusaoLogica,IdUnidade,IdImposto")] Leitura leitura)
//        {
//            if (id != leitura.Id)
//                return NotFound();

//            if (ModelState.IsValid)
//            {
//                Data.Entities.Afericao.TbLeitura tbLeitura = _mapper.Map<Data.Entities.Afericao.TbLeitura>(leitura);
//                try
//                {
//                    await _leituraService.Alterar(id, tbLeitura);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!LeituraExists(id))
//                        return NotFound();
//                    else
//                        throw;
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(leitura);
//        }

//        public async Task<IActionResult> Excluir(Guid? id)
//        {
//            if (id == null)
//                return NotFound();

//            Leitura leitura = _mapper.Map<Leitura>(await _leituraService.Buscar(id));

//            if (leitura == null)
//                return NotFound();

//            return View(leitura);
//        }

//        [HttpPost, ActionName("Excluir")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ExcluirConfirmed(Guid id)
//        {
//            await _leituraService.Excluir(await _leituraService.Buscar(id));
//            return RedirectToAction(nameof(Index));
//        }

//        private bool LeituraExists(Guid id)
//        {
//            if (_leituraService.Buscar(id) == null)
//                return false;
//            else
//                return true;
//        }

//        public async Task<IActionResult> Importar()
//        {
//            return await Task.Run(() => View());
//        }

//        public async Task<IActionResult> OnPostImportar()
//        {
//            IFormFile arquivo = Request.Form.Files[0];
//            string novoCaminho = Path.GetTempFileName();
//            if (arquivo.Length > 0)
//            {
//                string extensaoDoArquivo = Path.GetExtension(arquivo.FileName).ToLower();
//                ISheet planilha;
//                using (var stream = new FileStream(novoCaminho, FileMode.Create))
//                {
//                    await arquivo.CopyToAsync(stream);
//                    stream.Position = 0;
//                    IWorkbook workbook;
//                    if (extensaoDoArquivo == ".xls")
//                    {
//                        workbook = new HSSFWorkbook(stream); // Galera do Excel 97 até o 2k
//                    }
//                    else
//                    {
//                        workbook = new XSSFWorkbook(stream); // Excel 2007 em diante                        
//                    }
//                    planilha = workbook.GetSheetAt(0);
//                    int posicaoLinhaCabecalho = 3;
//                    IRow linhaDoCabecalho = planilha.GetRow(posicaoLinhaCabecalho);
//                    int contadorDeCelulas = linhaDoCabecalho.LastCellNum;
//                    var nomeCondominio = planilha.GetRow(1).GetCell(2).ToString();
//                    var condominioTb = _condominioService.BuscarPorNome(nomeCondominio);
//                    try
//                    {
//                        var unidadeTeste = _mapper.Map<Web.Models.Habitacao.Unidade>(_unidadeService.Buscar(Guid.Parse("2f511f96-95ad-4fe1-9ace-405aa444c18f")));
//                    }
//                    catch (Exception e)
//                    {
//                        Console.WriteLine("========================");
//                        Console.WriteLine(e.Message);
//                    }
//                    if (condominioTb != null)
//                    {
//                        var condominio = _mapper.Map<Web.Models.Habitacao.Condominio>(condominioTb);
//                        var dataAtual = planilha.GetRow(3).GetCell(3).DateCellValue;
//                        List<Leitura> leituras = new List<Leitura>();
//                        for (int i = (planilha.FirstRowNum + posicaoLinhaCabecalho); i <= planilha.LastRowNum; i++) // Faz a leitura do arquivo 
//                        {
//                            var leitura = new Leitura();
//                            leitura.Unidade = new Models.Habitacao.Unidade();
//                            IRow linha = planilha.GetRow(i);
//                            if (linha == null) continue;
//                            if (linha.Cells.All(x => x.CellType == CellType.Blank)) continue;
//                            for (int j = linha.FirstCellNum; j < contadorDeCelulas; j++)
//                            {
//                                if (linha.GetCell(j) != null)
//                                {
//                                    switch (j)
//                                    {
//                                        case 1:
//                                            leitura.Unidade.Numero = linha.GetCell(j).ToString();
//                                            continue;
//                                        case 2:
//                                            leitura.HidrometroAnterior = int.Parse(linha.GetCell(j).ToString());
//                                            continue;
//                                        case 3:
//                                            leitura.HidrometroAtual = int.Parse(linha.GetCell(j).ToString());
//                                            continue;
//                                    }
//                                }
//                            }
//                            leitura.Unidade = new Models.Habitacao.Unidade();
//                            leitura.Unidade.Cpf = "03603603691";
//                            leitura.RealizadaEm = dataAtual;
//                            leitura.NomeDaImagem = "empty";
//                            leitura.Unidade.Condominio = condominio;
//                            leitura.MetrosCubicos = leitura.HidrometroAtual - leitura.HidrometroAnterior;
//                            leituras.Add(leitura);
//                            if (i == 310)
//                                break;
//                        }
//                        await _leituraService.SalvarListaLeituras(_mapper.Map<List<Data.Entities.Afericao.TbLeitura>>(leituras));
//                    }
//                    else
//                    {
//                        Console.WriteLine("Condomínio não cadastrado.");
//                    }
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}