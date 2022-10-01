
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.MovieAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
internal sealed class MovierEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies", MovieContext.DEFAULTSCHEMA);

        builder.HasKey(u => u.Id);

        builder.Property(sp => sp.ImdbUrl)
            .HasMaxLength(2500);

        builder.HasData(Movie.InitialSeedData());
    }
}
