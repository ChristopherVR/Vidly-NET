using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", MovieContext.DEFAULT_SCHEMA);

        builder.HasKey(u => u.Id);

        builder.HasIndex(r => r.Username).IsUnique();

        builder.Metadata
            ?.FindNavigation(nameof(User.UserFavouriteMovies))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(u => u.UserDetails, cd => cd.WithOwner());

        builder.HasData(User.CreateInitialSeedData());
    }
}
