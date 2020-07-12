using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Signo.App.ViewModels;
using Signo.Business.Interfaces;
using Signo.Business.Models;

namespace Signo.App.Controllers
{
    public class IntegrantesController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IIntegranteRepository _integranteRepository;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly AspNetRoleManager<IdentityUser> _roleManager;





        public IntegrantesController(IMapper mapper,
            IIntegranteRepository integranteRepository,
           UserManager<IdentityUser> userManager)

        {

            _mapper = mapper;
            _integranteRepository = integranteRepository;
            _userManager = userManager;
        }







        public async Task<IActionResult> Index(string sortOrder,
            string searchStrNome,
            string searchStrUnidade,
            string searchStrFuncaoBordo,
            bool searchNaoAtivo,
            bool searchAtivo)

        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UnidadeSortParm = sortOrder == "Unidade" ? "unidade_desc" : "Unidade";
            ViewBag.MatriculaSortParm = sortOrder == "Matricula" ? "matricula_desc" : "Matricula";
            ViewBag.FuncaoBordoSortParm = sortOrder == "FuncaoBordo" ? "funcaoBordo_desc" : "FuncaoBordo";
            ViewBag.FuncaoContratoSortParm = sortOrder == "FuncaoContrato" ? "funcaoContrato_desc" : "FuncaoContrato";
            ViewBag.EmpresaSortParm = sortOrder == "Empresa" ? "empresa_desc" : "Empresa";

            //muda apenas o valor que esta na bag para a proxima vez que passar, o SORTORDER continua intacto para o Select Case

            IEnumerable<Integrante> integranteS = await _integranteRepository.ObterTodos();


            IEnumerable<Integrante> integrante = from s in integranteS
                                                 select s;


            if (!String.IsNullOrEmpty(searchStrNome) & !String.IsNullOrEmpty(searchStrUnidade) & !String.IsNullOrEmpty(searchStrFuncaoBordo))
            {
                integrante = integrante.Where(s => s.Nome.Contains(searchStrNome) &
                             s.Unidade.Contains(searchStrUnidade) &
                             s.FuncaoBordo.Contains(searchStrFuncaoBordo));
                goto Jump;
            }

            if (!String.IsNullOrEmpty(searchStrNome) & !String.IsNullOrEmpty(searchStrUnidade))
            {
                integrante = integrante.Where(s => s.Nome.Contains(searchStrNome) &
                                                   s.Unidade.Contains(searchStrUnidade));
                goto Jump;
            }
            if (!String.IsNullOrEmpty(searchStrNome) & !String.IsNullOrEmpty(searchStrFuncaoBordo))
            {
                integrante = integrante.Where(s => s.Nome.Contains(searchStrNome) &
                                                s.FuncaoBordo.Contains(searchStrFuncaoBordo));
                goto Jump;
            }

            if (!String.IsNullOrEmpty(searchStrNome))
            {
                integrante = integrante.Where(s => s.Nome.Contains(searchStrNome));
                goto Jump;
            }

            if (!String.IsNullOrEmpty(searchStrUnidade))
            {
                integrante = integrante.Where(s => s.Unidade.Contains(searchStrUnidade));
                goto Jump;
            }
            if (!String.IsNullOrEmpty(searchStrFuncaoBordo))
            {
                integrante = integrante.Where(s => s.FuncaoBordo.Contains(searchStrFuncaoBordo));
                goto Jump;
            }

            Jump:





            switch (sortOrder)
            {
                case "name_desc":

                    integrante = integrante.OrderByDescending(s => s.Nome);

                    break;
                case "Unidade":
                    integrante = integrante.OrderBy(s => s.Unidade);
                    break;
                case "unidade_desc":
                    integrante = integrante.OrderByDescending(s => s.Unidade);
                    break;
                case "Matricula":
                    integrante = integrante.OrderBy(s => s.Unidade);
                    break;
                case "matricula_desc":
                    integrante = integrante.OrderByDescending(s => s.Unidade);
                    break;
                case "FuncaoBordo":
                    integrante = integrante.OrderBy(s => s.Unidade);
                    break;
                case "funcaoBordo_desc":
                    integrante = integrante.OrderByDescending(s => s.Unidade);
                    break;
                case "FuncaoContrato":
                    integrante = integrante.OrderBy(s => s.Unidade);
                    break;
                case "funcaoContrato_desc":
                    integrante = integrante.OrderByDescending(s => s.Unidade);
                    break;
                case "Empresa":
                    integrante = integrante.OrderBy(s => s.Unidade);
                    break;
                case "empresa_desc":
                    integrante = integrante.OrderByDescending(s => s.Unidade);
                    break;
                default:
                    integrante = integrante.OrderBy(s => s.Nome);
                    break;
            }
            if (searchAtivo == true)
            {
                integrante = integrante.Where(s => s.Ativo == true);

            }
            else if (searchNaoAtivo == true)
            {
                integrante = integrante.Where(s => s.Ativo == false);
            }

            var integranteMapped = _mapper.Map<IEnumerable<IntegranteViewModel>>(integrante);

            return View(integranteMapped.ToList());
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var integrante = await _integranteRepository.ObterPorId(id);

            if (integrante == null)
            {
                return NotFound();
            }
            var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);


            return View(integranteMapped);
        }


        public IActionResult Create()
        {



            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IntegranteViewModel integranteViewModel)
        {
            IdentityUser usr = await GetCurrentUserAsync();
            integranteViewModel.Id = Guid.Parse(usr.Id);

            if (integranteViewModel != null)
            {

                //IdentityUser usr = await GetCurrentUserAsync();


                var imgPrefixo = Guid.Parse(usr.Id) + "_Foto";
                var imgPrefixo2 = Guid.Parse(usr.Id) + "_Ass";







                if (!await UploadArquivoImgFoto(integranteViewModel.ImgFotoUpload, imgPrefixo))
                {
                    return View(integranteViewModel);
                }
                integranteViewModel.ImgFoto = imgPrefixo + ".JPG";


                if (!await UploadArquivoSignFoto(integranteViewModel.ImgSignUpload, imgPrefixo2))
                {
                    return View(integranteViewModel);
                }
                integranteViewModel.ImgSign = imgPrefixo + ".JPG";


                UpperIntegranteVm(integranteViewModel);
                var integranteMapped = _mapper.Map<Integrante>(integranteViewModel);
                integranteMapped.Id = Guid.Parse(usr.Id);
                integranteMapped.Admissao = DateTime.Now;

                await _integranteRepository.Adicionar(integranteMapped);

                await _userManager.AddClaimAsync(usr, new Claim("PTW", "EMITENTE"));


                var code = await _userManager.GenerateEmailConfirmationTokenAsync(usr);
                await _userManager.ConfirmEmailAsync(usr, code);

                return RedirectToAction(nameof(Index));
            }
            return View(integranteViewModel);
        }


        public async Task<IActionResult> Edit(Guid id)
        {

            var integrante = await _integranteRepository.ObterPorId(id);
            var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);

            if (integrante == null)
            {
                return NotFound();
            }
            return View(integranteMapped);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, IntegranteViewModel integrante)
        {
            if (id != integrante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                var imgPrefixo = id + "_Foto";
                var imgPrefixo2 = id + "_Ass";

                var integranteImgs = await _integranteRepository.ObterPorId(id);
                integrante.ImgFoto = integranteImgs.ImgFoto;
                integrante.ImgSign = integranteImgs.ImgSign;

                if (integrante.ImgFotoUpload == null)
                {
                    goto Jfoto;
                }
                if (!await UploadArquivoImgFoto(integrante.ImgFotoUpload, imgPrefixo))
                {
                    return View(integrante);
                }

                integrante.ImgFoto =
                    imgPrefixo + ".JPG";
                Jfoto:
                if (integrante.ImgSignUpload == null)
                {
                    goto Jsign;
                }
                if (!await UploadArquivoSignFoto(integrante.ImgSignUpload, imgPrefixo2))
                {
                    return View(integrante);
                }
                integrante.ImgSign =
                    imgPrefixo2 + ".JPG";

                Jsign:
                try
                {
                    integrante.Admissao = DateTime.Now;

                    UpperIntegranteVm(integrante);




                    var integranteMapped = _mapper.Map<Integrante>(integrante);
                    await _integranteRepository.Atualizar(integranteMapped);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntegranteExists(integrante.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(integrante);
        }

        // GET: Integrantes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var integrante = await _integranteRepository.ObterPorId(id);

            if (integrante == null)
            {
                return NotFound();
            }

            var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);

            return View(integranteMapped);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            IdentityUser usr = await _userManager.FindByIdAsync(id.ToString());
            if (usr != null)
            {
                var filePrefixo = id.ToString();
                ApagaFotoUser(filePrefixo);
                ApagaAssUser(filePrefixo);
                await _userManager.DeleteAsync(usr);
                await _integranteRepository.Remover(id);
            }



            return RedirectToAction(nameof(Index));
        }

        private bool IntegranteExists(Guid id)
        {

            var ex = _integranteRepository.Buscar(x => x.Id == id);

            if (ex == null)
            {
                return false;
            }

            return true;
        }



        public async Task<IActionResult> EditClaims(Guid id)
        {


            IdentityUser usr = await _userManager.FindByIdAsync(id.ToString());


            var integrante = await _integranteRepository.ObterPorId(id);
            var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);

            if (integrante == null)
            {
                return NotFound();
            }
            IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(usr);

            integranteMapped.UserClaims = claims;
            ViewData["Normalized"] = integranteMapped.Nome.ToUpper();

            return View(integranteMapped);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelClaim(Guid id, IntegranteViewModel integranteViewModel)
        {
            IdentityUser usr = await _userManager.FindByIdAsync(id.ToString());


            if (String.IsNullOrEmpty(integranteViewModel.ManipulateUserClaimsType) &
                String.IsNullOrEmpty(integranteViewModel.ManipulateUserClaimsType))
            {
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Claim> claimsList = await _userManager.GetClaimsAsync(usr);
            if (!claimsList.Any()) { return NotFound("Ops, o usuário não possui Permissões para serem apagadas!"); }

            string claimType = integranteViewModel.ManipulateUserClaimsType;
            string claimValue = integranteViewModel.ManipulateUserClaimsValue;


            var integrante = await _integranteRepository.ObterPorId(id);
            var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);

            Claim claimUsr = new Claim(claimType, claimValue);
            await _userManager.RemoveClaimAsync(usr, claimUsr);

            ViewData["Normalized"] = integranteMapped.Nome.ToUpper();


            return RedirectToAction("EditClaims", new RouteValueDictionary(
                new { controller = "Integrantes", action = "EditClaims", id }));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClaims(Guid id, IntegranteViewModel integranteViewModel)
        {
            IdentityUser usr = await _userManager.FindByIdAsync(id.ToString());

            if (String.IsNullOrEmpty(integranteViewModel.ManipulateUserClaimsType) &
                String.IsNullOrEmpty(integranteViewModel.ManipulateUserClaimsType))
            {
                return RedirectToAction(nameof(Index));
            }

            string claimType = integranteViewModel.ManipulateUserClaimsType.ToUpper();
            string claimValue = integranteViewModel.ManipulateUserClaimsValue.ToUpper();


            Claim claimUsr = new Claim(claimType, claimValue);
            await _userManager.AddClaimAsync(usr, claimUsr);

            var integrante = await _integranteRepository.ObterPorId(id);
            ViewData["Normalized"] = integrante.Nome.ToUpper();

            return RedirectToAction("EditClaims", new RouteValueDictionary(
                new { controller = "Integrantes", action = "EditClaims", id }));



        }













        // Metodos Privados///////////////////////////////////

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        private async Task<bool> UploadArquivoImgFoto(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgFotos",
                imgPrefixo + ".JPG");    /*arquivo.FileName);*/

            if (System.IO.File.Exists(path))
            {

                {
                    ApagaFotoUser(imgPrefixo);
                }
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }



        private async Task<bool> UploadArquivoSignFoto(IFormFile arquivo, string imgPrefixo2)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgAss",
                imgPrefixo2 + ".JPG");       /*arquivo.FileName);*/

            if (System.IO.File.Exists(path))
            {

                {
                    ApagaAssUser(imgPrefixo2);
                }
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }


        private bool ApagaFotoUser(string filePrefixo)
        {

            var pathFotos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgFotos",
                 filePrefixo + "_Foto.JPG");       /*arquivo.FileName);*/

            if (System.IO.File.Exists(pathFotos))
            {

                {
                    System.IO.File.Delete(pathFotos);
                }
            }



            return true;
        }
        private bool ApagaAssUser(string filePrefixo)
        {

            var pathAss = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgAss",
                filePrefixo + "_Ass.JPG");       /*arquivo.FileName);*/

            if (System.IO.File.Exists(pathAss))
            {

                {
                    System.IO.File.Delete(pathAss);
                }
            }
            return true;
        }

        private IntegranteViewModel UpperIntegranteVm(IntegranteViewModel integranteView)
        {
            integranteView.Nome = integranteView.Nome.ToUpper();
            integranteView.Empresa = integranteView.Empresa.ToUpper();
            integranteView.Endereco = integranteView.Endereco.ToUpper();
            integranteView.FuncaoBordo = integranteView.FuncaoBordo.ToUpper();
            integranteView.FuncaoContrato = integranteView.FuncaoContrato.ToUpper();
            integranteView.Nacionalidade = integranteView.Nacionalidade.ToUpper();
            
           

            return (integranteView);
        }
    }
}
