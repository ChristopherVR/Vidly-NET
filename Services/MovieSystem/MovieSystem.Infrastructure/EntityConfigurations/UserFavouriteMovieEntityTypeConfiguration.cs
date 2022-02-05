using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
class UserFavouriteMovieEntityTypeConfiguration : IEntityTypeConfiguration<UserFavouriteMovie>
{
    public void Configure(EntityTypeBuilder<UserFavouriteMovie> builder)
    {
        builder.ToTable("UserFavouriteMovies", MovieContext.DEFAULT_SCHEMA);

        builder.HasKey(u => u.Id);
    }
}
