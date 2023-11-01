using Application.Interfaces;

namespace Infrastructure
{
    public static class NbpApiServiceFactory
    {
        public static INbpApiService Create()
        {
            return new NbpApiService();
        }
    }
}
