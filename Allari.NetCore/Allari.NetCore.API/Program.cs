using Allari.NetCore.Application;
using Allari.NetCore.Application.Validations.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()    
     .AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly))
    .AddMediatRApplicationValidation(AssemblyReference.Assembly);;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
