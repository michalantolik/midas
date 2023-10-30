namespace MidasRatesUpdater.Services.Database
{
    /// <summary>
    /// Service for accessing database.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Persists database changes.
        /// </summary>
        void Save();
    }
}
