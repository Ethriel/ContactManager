namespace ContactManager.Services.Abstraction
{
    public interface ICsvService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> ReadFromStream(Stream stream);
        Task<IEnumerable<TEntity>> ReadFromStreamAsync(Stream stream);
    }
}
