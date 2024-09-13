using ContactManager.Services.Abstraction;
using CsvHelper;
using CsvHelper.Configuration;
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
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    AllowComments = true,
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using (var csv = new CsvReader(reader, csvConfig))
                {
                    var records = csv.GetRecords<TEntity>();
                    return records.ToArray();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> ReadFromStreamAsync(Stream stream)
        {
            return await Task.FromResult(ReadFromStream(stream));
        }
    }
}
