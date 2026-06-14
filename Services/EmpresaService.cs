using ApiEmpresa.Models;
using ApiEmpresa.DTOs;
using ApiEmpresa.Services.Interfaces;
using ApiEmpresa.Repositories.Interfaces;

namespace ApiEmpresa.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Empresa?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<int> CreateAsync(CreateEmpresaDto dto)
        {
            // Validações de negócio ficam aqui
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome da empresa é obrigatório.");
            if (string.IsNullOrWhiteSpace(dto.Cnpj))
                throw new ArgumentException("CNPJ é obrigatório.");

            return await _repository.CreateAsync(dto);
        }

        public async Task<bool> UpdateAsync(int id, UpdateEmpresaDto dto) =>
            await _repository.UpdateAsync(id, dto);

        public async Task<bool> DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public async Task<bool> PatchAsync(int id, PatchEmpresaDto dto) =>
            await _repository.PatchAsync(id, dto);
    }
}