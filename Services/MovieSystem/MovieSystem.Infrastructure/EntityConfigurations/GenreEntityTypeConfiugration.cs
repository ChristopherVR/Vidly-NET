using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.GenreAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
class GenreEntityTypeConfiugration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres", MovieContext.DEFAULT_SCHEMA);

        builder.HasKey(u => u.Id);

        builder.HasData(Genre.CreateInitialSeedData());
    }
}
