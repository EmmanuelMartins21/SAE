using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SAE.Processor.Repositories;
using SAE.Shared;
using System.Text;

using System.Text.Json;

namespace SAE.Processor.Messaging
{
    public class ConsumidorAgendamento
    {
        private readonly IModel _canal;
        private readonly ILogger<ConsumidorAgendamento> _logger;

        public ConsumidorAgendamento(IModel canal, ILogger<ConsumidorAgendamento> logger)
        {
            _canal = canal;
            _logger = logger;
        }

        public void Iniciar()
        {
            _canal.QueueDeclare("agendamentos", false, false, false, null);

            var consumidor = new EventingBasicConsumer(_canal);
            consumidor.Received += async (model, ea) =>
            {
                var corpo = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(corpo);
                var dto = JsonSerializer.Deserialize<ExameAgendadoDto>(json);

                if (dto is null)
                {
                    _logger.LogWarning("⚠️ Mensagem recebida inválida.");
                    return;
                }

                _logger.LogInformation("📨 Mensagem recebida: {NomePaciente} - {TipoExame}", dto.NomePaciente, dto.TipoExame);

                try
                {
                    await AgendamentoRepository.GravarAsync(dto, _logger);
                    _logger.LogInformation("✔️ Agendamento salvo com sucesso.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Erro ao salvar agendamento no banco de dados.");
                }
            };

            _canal.BasicConsume(queue: "agendamentos", autoAck: true, consumer: consumidor);

            _logger.LogInformation("🎧 Aguardando mensagens da fila 'agendamentos'...");
        }
    }
}
