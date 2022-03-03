using MovieSystem.API.Application.Queries;
using Module = Autofac.Module;

namespace MovieSystem.API.Infrastructure.AutofacModules;

public class QueryModule : Module
{
    private readonly string _queryConnectionString;
    public QueryModule(string queryConnectionString)
    {
        _queryConnectionString = queryConnectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c => new MovieQueries(_queryConnectionString))
            .As<IMovieQueries>()
            .InstancePerLifetimeScope();

        builder.Register(c => new UserQueries(_queryConnectionString))
            .As<IUserQueries>()
            .InstancePerLifetimeScope();

        builder.Register(c => new GenreQueries(_queryConnectionString))
            .As<IGenreQueries>()
            .InstancePerLifetimeScope();
    }
}

