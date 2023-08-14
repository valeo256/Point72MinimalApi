using Application.Common.Mapping;
using AutoMapper;
using Infrastructure.Persistence;
using Xunit;

namespace Application.Tests.Integration;

public class QueryTestFixture : IDisposable
{
    public ApplicationDbContext Context { get; private set; }
    public IMapper Mapper { get; private set; }

    public QueryTestFixture()
    {
        Context = ApplicationContextFactory.Create();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        ApplicationContextFactory.Destroy(Context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> { }