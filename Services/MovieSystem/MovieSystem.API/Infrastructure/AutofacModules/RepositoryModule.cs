using MovieSystem.Infrastructure.Repositories;
using Autofac;
using MovieSystem.Domain.AggregatesModel.MovieAggregate;
using MovieSystem.Domain.AggregatesModel.UserAggregate;

namespace MovieSystem.API.Infrastructure.AutofacModules
{
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
}
