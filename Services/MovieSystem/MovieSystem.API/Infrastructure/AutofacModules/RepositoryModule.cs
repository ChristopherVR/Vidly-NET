using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using MovieSystem.Domain.AggregatesModel.UserAggregate;
using MovieSystem.Infrastructure.Repositories;
using Module = Autofac.Module;

namespace MovieSystem.API.Infrastructure.AutofacModules;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MovieRepository>()
            .As<IMovieRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserRepository>()
            .As<IUserRepository>()
            .InstancePerLifetimeScope();
    }
}

