using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceitaController : ControllerBase
    {

        private static List<Receita> receitas = new List<Receita>()
        {
            new Receita { ReceitaId = 1, NomeReceita = "Salada de Frango", Tipo = "Fit", Ingredientes = "Frango e alface" },
            new Receita { ReceitaId = 2, NomeReceita = "Quinoa com Legumes", Tipo = "Fit", Ingredientes =  "Quinoa, brócolis, cenoura e pimentão"},
            new Receita { ReceitaId = 3, NomeReceita = "Omelete de Espinafre", Tipo = "Fit", Ingredientes = "Ovos, espinafre, cebola e tomate" },
            new Receita { ReceitaId = 4, NomeReceita =  "Sopa de Lentilhas", Tipo = "Fit", Ingredientes = "Ovos, espinafre, cebola e tomate" },
            new Receita { ReceitaId = 5, NomeReceita =  "Wrap de Peito de Peru", Tipo = "Fit", Ingredientes = "Peito de peru, alface, tomate e tortilha integral" },
            new Receita { ReceitaId = 5, NomeReceita =  "Wrap de Peito de Peru", Tipo = "Fit", Ingredientes = "Peito de peru, alface, tomate e tortilha integral" },
            new Receita { ReceitaId = 6,NomeReceita = "Arroz Integral com Frango",Tipo = "Normal",Ingredientes = "Arroz integral, frango grelhado e brócolis"},
            new Receita {ReceitaId = 7, NomeReceita = "Smoothie de Frutas Vermelhas", Tipo = "Normal", Ingredientes = "Frutas vermelhas, iogurte natural e mel"},
            new Receita {ReceitaId = 8, NomeReceita = "Salada de Grão-de-Bico", Tipo = "Normal", Ingredientes = "Grão-de-bico, tomate, pepino e cebola roxa" },
            new Receita{ReceitaId = 9, NomeReceita = "Peixe Grelhado com Aspargos", Tipo = "Normal", Ingredientes = "Filé de peixe, aspargos e limão"},
            new Receita{ReceitaId = 10, NomeReceita = "Batata Doce Assada", Tipo = "Normal", Ingredientes = "Batata doce, azeite e alecrim"}
        };

        private readonly ILogger<ReceitaController> _logger;

        public ReceitaController(ILogger<ReceitaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{imc}", Name = "GetReceita")]
        public IActionResult Get(double imc)
        {
            Random rand = new Random();


            if (imc < 25)
            {
                var receitasNormal = receitas.Where(w => w.Tipo == "Normal").ToList();
                Receita receitaAleatoria = receitasNormal[rand.Next(receitasNormal.Count)];
                return new JsonResult(receitaAleatoria);
            }
            else
            {
                var receitasFit = receitas.Where(w => w.Tipo == "Fit").ToList(); ;
                Receita receitaAleatoria = receitasFit[rand.Next(receitasFit.Count)];
                return new JsonResult(receitaAleatoria);
            }
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(receitas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Receita novaReceita)
        {
            if (novaReceita == null)
            {
                return BadRequest("Dados Inválidos");
            }

            int novoId = receitas.Max(r => r.ReceitaId + 1);
            novaReceita.ReceitaId = novoId;
            receitas.Add(novaReceita);
            return Ok(novaReceita);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Receita receitaAtualizada)
        {
            if (receitaAtualizada == null)
            {
                return BadRequest("Dados Inválidos");
            }

            var receitaExistente = receitas.FirstOrDefault(r => r.ReceitaId == id);
            if (receitaExistente == null)
            {
                return NotFound("Receita não encontrada");
            }

            receitaExistente.NomeReceita = receitaAtualizada.NomeReceita;
            receitaExistente.Tipo = receitaAtualizada.Tipo;
            receitaExistente.Ingredientes = receitaAtualizada.Ingredientes;

            return Ok(receitaExistente);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var receita = receitas.FirstOrDefault(r => r.ReceitaId == id);
            if (receita == null)
            {
                return NotFound("Receita não encontrada");
            }

            receitas.Remove(receita);

            return NoContent();
        }
    }
}
