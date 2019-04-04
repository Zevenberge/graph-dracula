using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dracula.Repository.Impl
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DraculaDbContext _dbContext;
        public CountryRepository(DraculaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _dbContext.Country.ToListAsync();
        }

        public async Task<Country> GetByIso(string iso)
        {
            return await _dbContext.Country.SingleOrDefaultAsync(c => c.Iso == iso);
        }
    }
}