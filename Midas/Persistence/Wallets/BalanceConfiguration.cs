using Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Wallets
{
    public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
    {
        public void Configure(EntityTypeBuilder<Balance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CurrencyCode)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.HasOne(x => x.Wallet);

            builder.Navigation(x => x.Wallet)
                .IsRequired()
                .AutoInclude();

            builder.HasData(
                new
                {
                    Id = 1,
                    WalletId = 1,
                    CurrencyCode = "BSD",
                    Amount = 27225.53m
                },
                new
                {
                    Id = 2,
                    WalletId = 1,
                    CurrencyCode = "KMF",
                    Amount = 24536.25m
                },
                new
                {
                    Id = 3,
                    WalletId = 1,
                    CurrencyCode = "EGP",
                    Amount = 29852.98m
                }
            ); ;
        }
    }
}