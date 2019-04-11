module dracula.site.dependencies.countries;

import dracula.site.dependencies.graphql;
import dracula.site.dto.countries;

CountryDto[] getCountries()
{
    return query!(CountriesQuery, q{
        query {
            countries {
                iso 
                name
            }
        }
    }).countries;
}

private:

class CountriesQuery
{
    CountryDto[] countries;
}