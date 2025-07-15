using SAE.Shared;

namespace SAE.API.Messaging
{
    public interface IPublicadorMensagem
    {
        void PublicarMensagem(ExameAgendadoDto exameDto, string nomeFila);
    }
}
