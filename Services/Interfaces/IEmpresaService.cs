using ApiEmpresa.Models;
using ApiEmpresa.DTOs;

namespace ApiEmpresa.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateEmpresaDto dto);
        Task<bool> UpdateAsync(int id, UpdateEmpresaDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> PatchAsync(int id, PatchEmpresaDto dto);

    }
}