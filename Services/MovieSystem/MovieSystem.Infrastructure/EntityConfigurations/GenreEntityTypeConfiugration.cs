using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.GenreAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
internal sealed class GenreEntityTypeConfiugration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres", MovieContext.DEFAULTSCHEMA);

        builder.HasKey(u => u.Id);

        builder.HasData(Genre.CreateInitialSeedData());
    }
}
