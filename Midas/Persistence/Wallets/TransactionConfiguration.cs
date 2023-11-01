using Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .IsRequired();

            builder.Property(x => x.CurrencyCode)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRequired();
        }
    }
}
