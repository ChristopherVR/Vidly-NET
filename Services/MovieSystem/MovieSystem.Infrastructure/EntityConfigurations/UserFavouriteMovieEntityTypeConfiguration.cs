using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
internal sealed class UserFavouriteMovieEntityTypeConfiguration : IEntityTypeConfiguration<UserFavouriteMovie>
{
    public void Configure(EntityTypeBuilder<UserFavouriteMovie> builder)
    {
        builder.ToTable("UserFavouriteMovies", MovieContext.DefaultSchema);

        builder.Property("UserId").IsRequired(true);

        builder.HasKey(u => u.Id);
    }
}
