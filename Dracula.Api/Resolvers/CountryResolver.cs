using System.Collections.Generic;
using System.Threading.Tasks;
using Dracula.Domain;
using Dracula.Repository;
using HotChocolate;

namespace Dracula.Api.Resolvers
{
    public class CountryResolver
    {
        public async Task<IEnumerable<Country>> GetAll(
                     [Service]ICountryRepository repository)
        {
            return await repository.GetAll();
        }
    }
}