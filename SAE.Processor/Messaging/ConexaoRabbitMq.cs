using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE.Processor.Messaging
{
    public static class ConexaoRabbitMq
    {
        public static IConnection CriarConexao()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            return factory.CreateConnection();
        }
    }
}
