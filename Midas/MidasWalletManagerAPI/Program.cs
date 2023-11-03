using Application.CurrencyConversion;
using Application.Interfaces;
using Application.Wallets.Commands.ConvertRequest;
using Application.Wallets.Commands.DepositRequest;
using Application.Wallets.Commands.WithdrawRequest;
using Application.Wallets.Queries.GetWalletsList;
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
builder.Services.AddScoped<IGetWalletsListQuery, GetWalletsListQuery>();
builder.Services.AddScoped<IDepositRequestCommand, DepositRequestCommand>();
builder.Services.AddScoped<IWithdrawRequestCommand, WithdrawRequestCommand>();
builder.Services.AddScoped<IConvertRequestCommand, ConvertRequestCommand>();
builder.Services.AddScoped<ICurrencyConverter, CurrencyConverter>();

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
