using Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Wallets
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                .IsRequired();

            builder.HasMany(w => w.Balances)
                   .WithOne(b => b.Wallet)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(w => w.Balances)
                .AutoInclude();

            builder.HasMany(w => w.Transactions)
                   .WithOne(t => t.Wallet)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(w => w.Transactions)
                .AutoInclude();

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = "Atena"
                },
                new
                {
                    Id = 2,
                    Name = "Hermes"
                },
                new
                {
                    Id = 3,
                    Name = "Demeter"
                }
            );
        }
    }
}
