using Infrastructure.Data.VnvcRepos;
using MongoDB.Driver;

namespace API.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection RegisterMongoDbRepositories(this IServiceCollection servicesBuilder)
        {
            servicesBuilder.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var uri = s.GetRequiredService<IConfiguration>()["MongoUri"];
                return new MongoClient(uri);
            });
            servicesBuilder.AddSingleton<VaccineRepository>();
            servicesBuilder.AddSingleton<NgTiemRepository>();
            servicesBuilder.AddSingleton<MaDatMuaRepository>();

            return servicesBuilder;
        }
    }
}