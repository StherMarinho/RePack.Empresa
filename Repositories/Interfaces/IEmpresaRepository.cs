using ApiEmpresa.Models;
using ApiEmpresa.DTOs;

namespace ApiEmpresa.Repositories.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateEmpresaDto dto);
        Task<bool> UpdateAsync(int id, UpdateEmpresaDto dto);
        Task<bool> DeleteAsync(int id); // desativa (ativo = false)

        Task<bool> PatchAsync(int id, PatchEmpresaDto dto);
    }
}