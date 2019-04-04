using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;

namespace Dracula.Repository
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAll();
        Task<Country> GetByIso(string iso);
    }
}
