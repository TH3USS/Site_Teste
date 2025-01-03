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

        // Página inicial: Lista de pessoas cadastradas
        public async Task<IActionResult> Index()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            return View(pessoas);
        }

        // Formulário para cadastrar uma nova pessoa
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção contra CSRF
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                // Log para depuração
                Console.WriteLine("ModelState inválido.");
                return View(pessoa);
            }
            try
            {
                await _pessoaRepository.AddAsync(pessoa); // Insere a pessoa no banco
                return RedirectToAction(nameof(Index)); // Redireciona para a lista de pessoas
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Esse CPF ja foi cadastrado!";
                return View(pessoa);
            }
            
        }


        // Exibe o formulário para editar uma pessoa
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Atualiza a pessoa
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

        // Ação para excluir pessoa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            try
            {
                await _pessoaRepository.DeleteAsync(id);  // Chame o método de exclusão do repositório
                return RedirectToAction(nameof(Index));  // Redireciona para a lista de pessoas
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Erro ao excluir a pessoa. Tente novamente.");
                return RedirectToAction(nameof(Index)); // Volta para a página de lista
            }
        }

    }


}
