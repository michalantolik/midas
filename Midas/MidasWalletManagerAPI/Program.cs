using Application.Interfaces;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------------------------------
// Add framework services to the container.
// ----------------------------------------------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------------------------------------------------------------------
// Add application services to the container.
// ----------------------------------------------------------------------------------------

builder.Services.AddScoped<IDatabaseService, DatabaseService>();

// ----------------------------------------------------------------------------------------
// Configure the HTTP request pipeline.
// ----------------------------------------------------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
