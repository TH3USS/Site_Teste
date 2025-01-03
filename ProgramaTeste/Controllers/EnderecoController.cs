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
        public async Task<IActionResult> Index(int pessoaId)
        {
            var enderecos = await _enderecoRepository.GetByPessoaIdAsync(pessoaId);
            ViewBag.PessoaId = pessoaId;
            return View(enderecos);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Endereco endereco)
        {
            if (!ModelState.IsValid)
            { 
                return View(endereco);
            }

			try
			{
				await _enderecoRepository.AddAsync(endereco);
				return RedirectToAction(nameof(Index), new { pessoaId = endereco.PessoaId });
			}
			catch (Exception ex)
			{
                TempData["Message"] = "Erro ao cadastrar. Tenha certeza de usar somente numeros no CEP.";
				return RedirectToAction(nameof(Index), new { pessoaId = endereco.PessoaId });
			}

			
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Endereco endereco)
        {
            
            if (!ModelState.IsValid)
            {
                return View(endereco);
            }

			try
			{
				await _enderecoRepository.UpdateAsync(endereco);
				endereco = await _enderecoRepository.GetByIdAsync(id);
				return RedirectToAction(nameof(Index), new { pessoaId = endereco.PessoaId });
			}
			catch (Exception ex)
			{
                TempData["Message"] = "Erro ao editar. Tenha certeza de usar somente numeros no CEP.";
                endereco = await _enderecoRepository.GetByIdAsync(id);
                return RedirectToAction(nameof(Index), new { pessoaId = endereco.PessoaId });
            }

			
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEndereco(int id, int pessoaId)
        {
            try
            {
                await _enderecoRepository.DeleteAsync(id);
                return RedirectToAction(nameof(Index), new { pessoaId = pessoaId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "Erro ao excluir o endereço. Tente novamente.");
                return RedirectToAction(nameof(Index), new { pessoaId = pessoaId });
            }
        }

    }
}
