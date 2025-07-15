using FluentValidation;
using SAE.Shared;

namespace SAE.API.Validators
{
    public class ExameAgendadoValidator : AbstractValidator<ExameAgendadoDto>
    {
        public ExameAgendadoValidator()
        {
            RuleFor(e => e.NomePaciente)
                .NotEmpty().WithMessage("O nome do paciente é obrigatório.");

            RuleFor(e => e.EmailPaciente)
                .NotEmpty().WithMessage("O e-mail do paciente é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado é inválido.");

            RuleFor(e => e.DataDesejada)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("A data desejada não pode ser no passado.");
        }
    }
}
