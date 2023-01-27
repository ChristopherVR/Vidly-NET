using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", MovieContext.DefaultSchema);

        builder.HasKey(u => u.Id);

        builder.HasIndex(r => r.Username).IsUnique();

        builder.Metadata
            ?.FindNavigation(nameof(User.UserFavouriteMovies))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(p => p.UserDetails, ps =>
        {
            ps.WithOwner();

            ps.HasData(
                new
                {
                    UserId = 1,
                    Address = "12th Avenue nr 17",
                    PersonalNumber = "+27 79 507 2155",
                    HomeNumber = "0000 632198",
                    ImageUrl = (string?)null,
                });
        });

        builder.HasData(User.CreateInitialSeedData());
    }
}
