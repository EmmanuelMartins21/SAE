using RabbitMQ.Client;
using SAE.Shared;
using System.Text.Json;
using System.Text;

namespace SAE.API.Messaging
{
    public class RabbitMqPublicador : IPublicadorMensagem
    {
        public void PublicarMensagem(ExameAgendadoDto exameDto, string nomeFila)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var conexao = factory.CreateConnection();
            using var canal = conexao.CreateModel();

            canal.QueueDeclare(nomeFila, false, false, false, null);

            var objeto = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(exameDto));

            canal.BasicPublish(string.Empty, nomeFila, null, objeto);
        }
    }
}
