namespace MidasWalletManagerAPI.Services.Database.Common
{
    /// <summary>
    /// Interface used to mark and find DB entity classes easily.
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}
