using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                await _integranteRepository.Adicionar(integranteMapped);

                await _userManager.AddClaimAsync(usr, new Claim("ptw", "emitente"));
              

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
                try
                {

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





     
      
        public async Task<IActionResult> DelClaim(Guid id)
        {
            IdentityUser usr = await _userManager.FindByIdAsync(id.ToString());


            var integrante = await _integranteRepository.ObterPorId(id);
            var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);

            if (integrante == null)
            {
                return NotFound();
            }

            await _userManager.RemoveClaimAsync(usr, new Claim("", ""));

            ViewData["Normalized"] = integranteMapped.Nome.ToUpper();

            return RedirectToAction(nameof(Index));
        }




        //[HttpPost]
        // public async Task<IActionResult> DelClaims(Guid id)
        // {
        //     string TypeClaim = "";
        //     string ValueClaim = "";



        //     IdentityUser usr = await _userManager.FindByIdAsync(id.ToString());


        //     var integrante = await _integranteRepository.ObterPorId(id);
        //     var integranteMapped = _mapper.Map<IntegranteViewModel>(integrante);

        //     if (integrante == null)
        //     {
        //         return NotFound();
        //     }



        //     if (TypeClaim==null || ValueClaim==null)
        //     {
        //         return NotFound("Não existem permissões para esta ação");
        //     }
        //     await _userManager.RemoveClaimAsync(usr, new Claim(TypeClaim, ValueClaim));

        //     ViewData["Normalized"] = integranteMapped.Nome.ToUpper();

        //     return RedirectToAction(nameof(Index));
        // }













        // Metodos Privados///////////////////////////////////

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



        private async Task<bool> UploadArquivoImgFoto(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgFotos",
                imgPrefixo + Path.GetExtension(arquivo.FileName));     /*arquivo.FileName);*/

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
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
                imgPrefixo2 + Path.GetExtension(arquivo.FileName));     /*arquivo.FileName);*/

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
        
      
    }
}
