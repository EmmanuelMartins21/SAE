using FluentValidation;
using SAE.API.Messaging;
using SAE.API.Services;
using SAE.API.Validators;
using SAE.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependências
builder.Services.AddScoped<IPublicadorMensagem, RabbitMqPublicador>();
builder.Services.AddScoped<AgendamentoService>();

// FluentValidation
builder.Services.AddScoped<IValidator<ExameAgendadoDto>, ExameAgendadoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

