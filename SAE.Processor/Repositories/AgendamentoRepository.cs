using Microsoft.Extensions.Logging;
using SAE.Processor.Data;
using SAE.Processor.Entities;
using SAE.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE.Processor.Repositories
{
    public class AgendamentoRepository
    {
        public static async Task GravarAsync(ExameAgendadoDto dto, ILogger logger)
        {
            logger.LogDebug("🔧 Iniciando persistência do agendamento...");

            using var contexto = new AgendamentoDbContext();

            var agendamento = new Agendamento
            {
                NomePaciente = dto.NomePaciente,
                TipoExame = dto.TipoExame,
                DataDesejada = dto.DataDesejada
            };

            contexto.Add(agendamento);
            await contexto.SaveChangesAsync();

            logger.LogDebug("💾 Persistência concluída com sucesso.");
        }
    }
}
