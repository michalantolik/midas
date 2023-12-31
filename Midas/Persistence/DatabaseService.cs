﻿using Application.Interfaces;
using Domain.ExchangeRates;
using Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.ExchangeRates;
using Persistence.Wallets;

namespace Persistence
{
    /// <inheritdoc />
    public class DatabaseService : DbContext, IDatabaseService
    {
        private const string SqlDbConnectionStringName = "MidasSqlDbConnectionString";

        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        /// <inheritdoc />
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        /// <inheritdoc />
        public DbSet<Wallet> Wallets { get; set; }

        /// <inheritdoc />
        public DbSet<Balance> Balances { get; set; }

        /// <inheritdoc />
        public DbSet<Transaction> Trasactions { get; set; }

        /// <inheritdoc />
        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString(SqlDbConnectionStringName);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new BalanceConfiguration().Configure(builder.Entity<Balance>());
            new ExchangeRateConfiguration().Configure(builder.Entity<ExchangeRate>());
            new TransactionConfiguration().Configure(builder.Entity<Transaction>());
            new WalletConfiguration().Configure(builder.Entity<Wallet>());
        }
    }
}
