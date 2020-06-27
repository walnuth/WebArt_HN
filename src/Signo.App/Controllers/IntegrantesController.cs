using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create(IntegranteViewModel integrante)
        {
            if (ModelState.IsValid)
            {
                var integranteMapped = _mapper.Map<Integrante>(integrante);

                IdentityUser usr = await GetCurrentUserAsync();

                integranteMapped.Id = Guid.Parse(usr.Id);

                await _integranteRepository.Adicionar(integranteMapped);

                return RedirectToAction(nameof(Index));
            }
            return View(integrante);
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

           var ex= _integranteRepository.Buscar(x => x.Id == id);

           if (ex==null) {
               return false;
           }

           return true;
        }




        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
