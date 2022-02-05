﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieSystem.Domain.AggregatesModel.MovieAggregate;

namespace MovieSystem.Infrastructure.EntityConfigurations;
class MovierEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies", MovieContext.DEFAULT_SCHEMA);

        builder.HasKey(u => u.Id);

        builder.Property(sp => sp.ImdbUrl)
            .HasMaxLength(2500);

        builder.HasData(Movie.InitialSeedData());
    }
}