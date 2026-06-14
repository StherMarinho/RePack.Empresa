using Microsoft.AspNetCore.Mvc;
using ApiEmpresa.DTOs;
using ApiEmpresa.Services.Interfaces;
using System.Security.Cryptography;

namespace ApiEmpresa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empresas = await _service.GetAllAsync();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empresa = await _service.GetByIdAsync(id);
            if (empresa == null) return NotFound();
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmpresaDto dto)
        {
            try
            {
                var id = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmpresaDto dto)
        {
            var sucesso = await _service.UpdateAsync(id, dto);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeleteAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] PatchEmpresaDto dto)
        {
            var sucesso = await _service.PatchAsync(id, dto);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}