using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using SAE.Processor.Messaging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton(ConexaoRabbitMq.CriarConexao());
        services.AddSingleton(provider =>
        {
            var conexao = provider.GetRequiredService<IConnection>();
            return conexao.CreateModel();
        });

        services.AddSingleton<ConsumidorAgendamento>();
    })
    .Build();

var consumidor = host.Services.GetRequiredService<ConsumidorAgendamento>();

consumidor.Iniciar();