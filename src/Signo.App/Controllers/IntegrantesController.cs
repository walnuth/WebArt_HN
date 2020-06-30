using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Signo.App.Data;
using Signo.App.ViewModels;
using Signo.Business.Interfaces;
using Signo.Business.Models;

namespace Signo.App.Controllers
{
    public class IntegrantesController : Controller
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







        public async Task<IActionResult> Index()
        {
            var integrante = await _integranteRepository.ObterTodos();
            var integranteMapped = _mapper.Map<IEnumerable<IntegranteViewModel>>(integrante);



            return View(integranteMapped);
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

            if (ModelState.IsValid)
            {

                IdentityUser usr = await GetCurrentUserAsync();


                var imgPrefixo = Guid.Parse(usr.Id) + "_Foto";
                var imgPrefixo2 = Guid.Parse(usr.Id) + "_Ass";







                if (!await UploadArquivoImgFoto(integranteViewModel.ImgFotoUpload, imgPrefixo))
                {
                    return View(integranteViewModel);
                }
                integranteViewModel.ImgFoto =
                    imgPrefixo + Path.GetExtension(integranteViewModel.ImgSignUpload.FileName);



                if (!await UploadArquivoSignFoto(integranteViewModel.ImgSignUpload, imgPrefixo2))
                {
                    return View(integranteViewModel);
                }
                integranteViewModel.ImgSign =
                    imgPrefixo2 + Path.GetExtension(integranteViewModel.ImgSignUpload.FileName);

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
                var imgPrefixo = id + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss",
                    CultureInfo.InvariantCulture) + "_Foto";
                var imgPrefixo2 = id + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss",
                    CultureInfo.InvariantCulture) + "_Ass";

                var integranteImgs = await _integranteRepository.ObterPorId(id);
                integrante.ImgFoto = integranteImgs.ImgFoto;
                integrante.ImgSign = integranteImgs.ImgSign;

                if (integrante.ImgFotoUpload== null)
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

            await _integranteRepository.Remover(id);

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

            IEnumerable<Claim> claimsList = await _userManager.GetClaimsAsync(usr);
            if (!claimsList.Any()) { return NotFound("Ops, o usuário não possui Permissões para serem apagadas!"); }

            string claimType = integranteViewModel.ManipulateUserClaimsType;
            string claimValue = integranteViewModel.ManipulateUserClaimsValue;
            if (claimType == null || claimValue == null) { return NotFound("Ops, você não pode deixar campos em branco!"); }

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

            string claimType = integranteViewModel.ManipulateUserClaimsType.ToUpper();
            string claimValue = integranteViewModel.ManipulateUserClaimsValue.ToUpper();
            if (claimType == null || claimValue == null) { return NotFound("Ops, você não pode deixar campos em branco!"); }

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
                    System.IO.File.Delete(path);
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
                   System.IO.File.Delete(path);
                }
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }


    }
}
