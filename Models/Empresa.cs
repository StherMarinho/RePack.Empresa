namespace ApiEmpresa.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool Ativo { get; set; }
    }
}