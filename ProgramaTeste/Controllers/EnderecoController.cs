using Microsoft.AspNetCore.Mvc;
using ProgramaTeste.Models;
using ProgramaTeste.Repositories;

namespace ProgramaTeste.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly EnderecoRepository _enderecoRepository;

        public EnderecoController(EnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        // Lista de endereços de uma pessoa
        public async Task<IActionResult> Index(int pessoaId)
        {
            var enderecos = await _enderecoRepository.GetByPessoaIdAsync(pessoaId);
            ViewBag.PessoaId = pessoaId; // Passa o ID da pessoa para a view
            return View(enderecos);
        }


        // POST: Cadastro de um novo endereço
        [HttpPost]
        public async Task<IActionResult> Create(Endereco endereco)
        {
            if (!ModelState.IsValid)
            { // Certifique-se de que o valor seja mantido
                return View(endereco);
            }

            await _enderecoRepository.AddAsync(endereco);
            // Após cadastrar o endereço, retorna à lista de endereços da pessoa
            return RedirectToAction(nameof(Index), new { pessoaId = endereco.PessoaId });
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Endereco endereco)
        {
            
            if (!ModelState.IsValid)
            {
                return View(endereco);
            }

            await _enderecoRepository.UpdateAsync(endereco);
            endereco = await _enderecoRepository.GetByIdAsync(id);
            return RedirectToAction(nameof(Index), new { pessoaId = endereco.PessoaId });
        }

        // Ação para excluir endereço
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEndereco(int id, int pessoaId)
        {
            try
            {
                await _enderecoRepository.DeleteAsync(id);  // Chama o método de exclusão do repositório
                return RedirectToAction(nameof(Index), new { pessoaId = pessoaId });  // Redireciona para a lista de endereços
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Erro ao excluir o endereço. Tente novamente.");
                return RedirectToAction(nameof(Index), new { pessoaId = pessoaId });  // Volta para a página de lista
            }
        }

    }
}
