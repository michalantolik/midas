using Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Wallets
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.HasMany(x => x.Balances);

            builder.Navigation(x => x.Balances)
                .AutoInclude();

            builder.HasMany(x => x.Transactions);

            builder.Navigation(x => x.Transactions)
                .AutoInclude();
        }
    }
}
