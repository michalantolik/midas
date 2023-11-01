using Domain.Wallets;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Persistence.Wallets
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Wallet);

            builder.Navigation(x => x.Wallet)
                .IsRequired()
                .AutoInclude();

            builder.Property(x => x.TransactionType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.CurrencyCode)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.Timestamp)
            .IsRequired();

            string dateFormat = "M/d/yyyy h:mm:ss tt zzz";
            builder.HasData(
                new
                {
                    Id = 1,
                    WalletId = 1,
                    TransactionType = TransactionType.Deposit,
                    CurrencyCode = "BSD",
                    Amount = 425.53m,
                    Timestamp = DateTimeOffset.ParseExact("11/1/2022 8:53:07 PM +01:00", dateFormat, null)
                },
                new
                {
                    Id = 2,
                    WalletId = 1,
                    TransactionType = TransactionType.Withdrawal,
                    CurrencyCode = "BSD",
                    Amount = 200.00m,
                    Timestamp = DateTimeOffset.ParseExact("11/7/2022 4:43:12 AM +01:00", dateFormat, null)
                },
                new
                {
                    Id = 3,
                    WalletId = 1,
                    TransactionType = TransactionType.Deposit,
                    CurrencyCode = "BSD",
                    Amount = 27000.00m,
                    Timestamp = DateTimeOffset.ParseExact("12/27/2022 2:22:05 PM +01:00", dateFormat, null)
                },
                new
                {
                    Id = 4,
                    WalletId = 1,
                    TransactionType = TransactionType.Deposit,
                    CurrencyCode = "KMF",
                    Amount = 24536.25m,
                    Timestamp = DateTimeOffset.Parse("06/05/2022 12:59:55 PM +01:00"),
                },
                new
                {
                    Id = 5,
                    WalletId = 1,
                    TransactionType = TransactionType.Deposit,
                    CurrencyCode = "EGP",
                    Amount = 29852.98m,
                    Timestamp = DateTimeOffset.Parse("01/21/2022 5:24:15 PM +01:00"),
                }
            );
        }
    }
}
