using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducentConsumerProblem
{
    internal class CountryHandler
    {

        CountryDAO countryDAO = new CountryDAO();

        public void SaveCountryData(
            int continentId,
            int allianceId,
            string name,
            int population,
            int area,
            int gdp
            )
        {


            Country country = new Country(continentId, allianceId, name, population, area, gdp);
            countryDAO.Save(country);


        }

        public void DeleteCountryData(int continentId, string name)
        {
            countryDAO.Delete(continentId, name);
            
   
        }

        public void UpdateCountryData(int continentId, int allianceId, string name, int population, int area, int gdp)
        {
            Country country = new Country( continentId,  allianceId,  name,  population,  area,  gdp);
            countryDAO.Update(country);
            //nsole.WriteLine($"Updating country data: {name}.");
            // Add database or storage logic here
        }
    }
}
