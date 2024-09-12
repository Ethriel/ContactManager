using ContactManager.Services.Abstraction;
using CsvHelper;
using System.Globalization;

namespace ContactManager.Services.Implementation
{
    public class CsvService<TEntity> : ICsvService<TEntity> where TEntity : class
    {
        public CsvService()
        {

        }

        public IEnumerable<TEntity> ReadFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<TEntity>();
                    return records;
                }
            }
        }

        public async Task<IEnumerable<TEntity>> ReadFromStreamAsync(Stream stream)
        {
            return await Task.FromResult(ReadFromStream(stream));
        }
    }
}
