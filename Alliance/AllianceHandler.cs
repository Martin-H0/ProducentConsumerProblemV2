using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducentConsumerProblem
{
    internal class AllianceHandler
    {
        AllianceDAO allianceDAO = new AllianceDAO();

        public void SaveAllianceData(
        string name,
        string type
    )
        {


            Alliance alliance = new Alliance(name, type);
            allianceDAO.Save(alliance);


        }


        /*
        public void DeleteContinentData(int id)
        {
            allianceDAO.Delete(id);


        }

        public void UpdateContinentData(
            string name,
            string type
            )
        {
            Alliance c = new Alliance(name, type);
            allianceDAO.Update(c);
            //nsole.WriteLine($"Updating country data: {name}.");
            // Add database or storage logic here
        }

        */



    }
}
