using Dapper;
using MySql.Data.MySqlClient;
using ApiEmpresa.Models;
using ApiEmpresa.DTOs;
using ApiEmpresa.Repositories.Interfaces;

namespace ApiEmpresa.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly string _connectionString;

        public EmpresaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        private MySqlConnection GetConnection() =>
            new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            using var conn = GetConnection();
            return await conn.QueryAsync<Empresa>(
                "SELECT * FROM empresa WHERE ativo = true");
        }

        public async Task<Empresa?> GetByIdAsync(int id)
        {
            using var conn = GetConnection();
            return await conn.QueryFirstOrDefaultAsync<Empresa>(
                "SELECT * FROM empresa WHERE id = @Id AND ativo = true",
                new { Id = id });
        }

        public async Task<int> CreateAsync(CreateEmpresaDto dto)
        {
            using var conn = GetConnection();
            var sql = @"INSERT INTO empresa 
                (nome, cnpj, telefone, email, cep, logradouro, numero, 
                 bairro, cidade, estado, latitude, longitude, ativo)
                VALUES 
                (@Nome, @Cnpj, @Telefone, @Email, @Cep, @Logradouro, @Numero,
                 @Bairro, @Cidade, @Estado, @Latitude, @Longitude, true);
                SELECT LAST_INSERT_ID();";
            return await conn.ExecuteScalarAsync<int>(sql, dto);
        }

        public async Task<bool> UpdateAsync(int id, UpdateEmpresaDto dto)
        {
            using var conn = GetConnection();
            var sql = @"UPDATE empresa SET
                nome = @Nome, cnpj = @Cnpj, telefone = @Telefone,
                email = @Email, cep = @Cep, logradouro = @Logradouro,
                numero = @Numero, bairro = @Bairro, cidade = @Cidade,
                estado = @Estado, latitude = @Latitude, longitude = @Longitude,
                ativo = @Ativo
                WHERE id = @Id";
            var rows = await conn.ExecuteAsync(sql, new
            {
                dto.Nome,
                dto.Cnpj,
                dto.Telefone,
                dto.Email,
                dto.Cep,
                dto.Logradouro,
                dto.Numero,
                dto.Bairro,
                dto.Cidade,
                dto.Estado,
                dto.Latitude,
                dto.Longitude,
                dto.Ativo,
                Id = id
            });
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = GetConnection();
            var rows = await conn.ExecuteAsync(
                "UPDATE empresa SET ativo = false WHERE id = @Id",
                new { Id = id });
            return rows > 0;
        }

        public async Task<bool> PatchAsync(int id, PatchEmpresaDto dto)
        {
            var campos = new List<string>();
            var parametros = new DynamicParameters();
            parametros.Add("Id", id);

            if (dto.Nome != null) { campos.Add("nome = @Nome"); parametros.Add("Nome", dto.Nome); }
            if (dto.Cnpj != null) { campos.Add("cnpj = @Cnpj"); parametros.Add("Cnpj", dto.Cnpj); }
            if (dto.Telefone != null) { campos.Add("telefone = @Telefone"); parametros.Add("Telefone", dto.Telefone); }
            if (dto.Email != null) { campos.Add("email = @Email"); parametros.Add("Email", dto.Email); }
            if (dto.Cep != null) { campos.Add("cep = @Cep"); parametros.Add("Cep", dto.Cep); }
            if (dto.Logradouro != null) { campos.Add("logradouro = @Logradouro"); parametros.Add("Logradouro", dto.Logradouro); }
            if (dto.Numero != null) { campos.Add("numero = @Numero"); parametros.Add("Numero", dto.Numero); }
            if (dto.Bairro != null) { campos.Add("bairro = @Bairro"); parametros.Add("Bairro", dto.Bairro); }
            if (dto.Cidade != null) { campos.Add("cidade = @Cidade"); parametros.Add("Cidade", dto.Cidade); }
            if (dto.Estado != null) { campos.Add("estado = @Estado"); parametros.Add("Estado", dto.Estado); }
            if (dto.Latitude != null) { campos.Add("latitude = @Latitude"); parametros.Add("Latitude", dto.Latitude); }
            if (dto.Longitude != null) { campos.Add("longitude = @Longitude"); parametros.Add("Longitude", dto.Longitude); }
            if (dto.Ativo != null) { campos.Add("ativo = @Ativo"); parametros.Add("Ativo", dto.Ativo); }

            if (!campos.Any()) return false; // nenhum campo enviado

            var sql = $"UPDATE empresa SET {string.Join(", ", campos)} WHERE id = @Id";

            using var conn = GetConnection();
            var rows = await conn.ExecuteAsync(sql, parametros);
            return rows > 0;
        }
    }
}