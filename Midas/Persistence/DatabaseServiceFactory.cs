using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public static class DatabaseServiceFactory
    {
        public static IDatabaseService Create(IConfiguration configuration)
        {
            return new DatabaseService(configuration);
        }
    }
}
