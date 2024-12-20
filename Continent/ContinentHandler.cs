using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducentConsumerProblem
{
    internal class ContinentHandler
    {
        ContinentDAO continentDAO = new ContinentDAO();

        public void SaveContinentData(
        string name

            )
        {


            Continent continent = new Continent(name);
            continentDAO.Save(continent);


        }
        /*
        public void DeleteContinentData( int id)
        {
            continentDAO.Delete( id);


        }

        public void UpdateContinentData(string name)
        {
            Continent c = new Continent(name);
            continentDAO.Update(c);
            //nsole.WriteLine($"Updating country data: {name}.");
            // Add database or storage logic here
        }
        */

    }
}
