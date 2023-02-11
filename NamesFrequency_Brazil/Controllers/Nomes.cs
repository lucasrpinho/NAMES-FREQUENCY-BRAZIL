using Microsoft.AspNetCore.Mvc;
using NamesFrequency_Brazil.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NamesFrequency_Brazil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Nomes : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<NomeStatsMax[]> Get(string name, string sexo)
        {
            try
            {
                var nome = name.Trim();
                if (nome == null) { throw new Exception("Informe o nome!"); }
                using var client = new HttpClient();

                string url = $"https://servicodados.ibge.gov.br/api/v1/censos/nomes/basica?nome={nome}&sexo={sexo}";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    if (result != null)
                    {
                        return JsonSerializer.Deserialize<NomeStatsMax[]>(result);
                    }
                }
                else
                {
                    throw new Exception("A requisição falhou. Código do erro: " + response.StatusCode);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
