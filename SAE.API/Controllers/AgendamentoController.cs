using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using SAE.Shared;
using SAE.API.Services;

namespace SAE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendamentoService _agendamentoService;

        public AgendamentoController(AgendamentoService agendamentoServico)
        {
            _agendamentoService = agendamentoServico;
        }

        [HttpPost]
        public IActionResult Agendar([FromBody] ExameAgendadoDto exameDto)
        {
            var resultado = _agendamentoService.ProcessarAgendamento(exameDto);

            if (!resultado.sucesso)
                return BadRequest(resultado.mensagem);

            return Ok(resultado.mensagem);
        }
    }
}
