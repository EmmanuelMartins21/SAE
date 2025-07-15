using FluentValidation;
using SAE.API.Messaging;
using SAE.Shared;

namespace SAE.API.Services;

public class AgendamentoService
{
    private readonly IPublicadorMensagem _publicadorMensagem;
    private readonly IValidator<ExameAgendadoDto> _validador;
    private readonly ILogger<AgendamentoService> _logger;

    public AgendamentoService(IPublicadorMensagem publicadorMensagem, IValidator<ExameAgendadoDto> validador, ILogger<AgendamentoService> logger)
    {
        _publicadorMensagem = publicadorMensagem;
        _validador = validador;
        _logger = logger;
    }

    public (bool sucesso, string mensagem) ProcessarAgendamento(ExameAgendadoDto exameDto)
    {
        _logger.LogInformation("Iniciando validação do agendamento...");

        var resultadoValidacao = _validador.Validate(exameDto);

        if (!resultadoValidacao.IsValid)
        {
            var mensagemErro = string.Join("; ", resultadoValidacao.Errors.Select(e => e.ErrorMessage));
            _logger.LogWarning("Validação falhou: {Erros}", mensagemErro);
            return (false, mensagemErro);
        }

        var nomeFila = exameDto.TipoExame switch
        {
            TipoExame.HEMOGRAMA_COMPLETO => "fila.hemograma",
            TipoExame.BETA_HCG => "fila.beta_hcg",
            TipoExame.IST => "fila.ist",
            _ => "fila.agendamentos"
        };

        _logger.LogInformation("Publicando agendamento na fila {Fila}", nomeFila);
        _publicadorMensagem.PublicarMensagem(exameDto, nomeFila);

        return (true, $" Agendamento enviado com sucesso para a fila {nomeFila}.");
    }
}
