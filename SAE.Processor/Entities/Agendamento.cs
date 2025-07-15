using SAE.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE.Processor.Entities
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string NomePaciente { get; set; } = string.Empty;
        public TipoExame TipoExame { get; set; } = 0;
        public DateTime DataDesejada { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
