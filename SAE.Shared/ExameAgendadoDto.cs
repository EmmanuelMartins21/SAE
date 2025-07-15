using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE.Shared;

public class ExameAgendadoDto
{
    public string NomePaciente { get; set; } = string.Empty;
    public string EmailPaciente { get; set; } = string.Empty;
    public TipoExame TipoExame { get; set; } =0;
    public DateTime DataDesejada { get; set; }
}
