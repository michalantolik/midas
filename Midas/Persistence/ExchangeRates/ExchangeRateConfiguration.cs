using Domain.ExchangeRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.ExchangeRates
{
    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Currency)
                .IsRequired();

            builder.Property(x => x.Code)
                .IsRequired();

            builder.Property(x => x.Mid)
                .HasColumnType("decimal(10, 6)")
                .IsRequired();

            builder.Property(x => x.TableName)
                .IsRequired();

            builder.Property(x => x.TableNo)
                .IsRequired();
            builder.Property(x => x.EffectiveDate)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .IsRequired();
        }
    }
}
