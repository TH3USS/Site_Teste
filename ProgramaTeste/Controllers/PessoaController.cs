using Microsoft.AspNetCore.Mvc;
using ProgramaTeste.Models;
using ProgramaTeste.Repositories;

namespace ProgramaTeste.Controllers
{
    public class PessoaController : Controller
    {
        private readonly PessoaRepository _pessoaRepository;

        public PessoaController(PessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            return View(pessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState inválido.");
                return View(pessoa);
            }
            try
            {
                await _pessoaRepository.AddAsync(pessoa);
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Esse CPF ja foi cadastrado!";
                return View(pessoa);
            }
            
        }


        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return View(pessoa);
            }
            
            try
            {
                await _pessoaRepository.UpdateAsync(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Esse CPF ja foi cadastrado!";
                return View(pessoa);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            try
            {
                await _pessoaRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Erro ao excluir a pessoa. Tente novamente.");
                return RedirectToAction(nameof(Index));
            }
        }

    }


}
