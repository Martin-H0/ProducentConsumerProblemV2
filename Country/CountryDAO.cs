using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;



namespace ProducentConsumerProblem
{
    public class CountryDAO
    {
        public IEnumerable<Country> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Country", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Country country = new Country(
                        Convert.ToInt32(reader[0].ToString()),
                        Convert.ToInt32(reader[1].ToString()),
                        Convert.ToInt32(reader[2].ToString()),
                        reader[3].ToString(),
                        Convert.ToInt32(reader[4].ToString()),
                        Convert.ToDouble(reader[5].ToString()),
                        Convert.ToDouble(reader[6].ToString())  
                    );
                    yield return country;
                }
                reader.Close();
            }
        }

        public void Save(Country c)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            try
            {
                using (command = new SqlCommand("insert into Country (continent_id, alliance_id, name, population, area, gdp) values (@continent_id, @alliance_id, @name, @population, @area, @gdp)", conn))
                {
                    command.Parameters.Add(new SqlParameter("@continent_id", c.Continent_id));
                    command.Parameters.Add(new SqlParameter("@alliance_id", c.Alliance_id));
                    command.Parameters.Add(new SqlParameter("@name", c.Name));
                    command.Parameters.Add(new SqlParameter("@population", c.Population));
                    command.Parameters.Add(new SqlParameter("@area", c.Area));
                    command.Parameters.Add(new SqlParameter("@gdp", c.Gdp));

                    command.ExecuteNonQuery();
                }
            }
            catch (System.InvalidOperationException)
            {
                // MLČ TY KOKOS
            }
        }

        public void Delete(int continentId, string name)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Country WHERE Continent_ID  = @id  AND  Name = @name", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", continentId));
                command.Parameters.Add(new SqlParameter("@name", name));

                command.ExecuteNonQuery();
            }
        }

        public void Update(Country c)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            using (command = new SqlCommand("update Country set alliance_id = @alliance_id,  population = @population, area = @area, gdp = @gdp " + "where Continent_ID  = @continent_id  AND  Name = @name", conn))
            {
                
                command.Parameters.Add(new SqlParameter("@continent_id", c.Continent_id));
                command.Parameters.Add(new SqlParameter("@alliance_id", c.Alliance_id));
                command.Parameters.Add(new SqlParameter("@name", c.Name));
                command.Parameters.Add(new SqlParameter("@population", c.Population));
                command.Parameters.Add(new SqlParameter("@area", c.Area));
                command.Parameters.Add(new SqlParameter("@gdp", c.Gdp));

                command.ExecuteNonQuery();
            }
        }

        public int GetId(int ContinentID, string CountryName)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT ID FROM Country WHERE Country.Name = @NameX AND Country.Continent_ID = @Continent_IDX", conn))
            {
                command.Parameters.Add(new SqlParameter("@NameX", CountryName));
                command.Parameters.Add(new SqlParameter("@Continent_IDX", ContinentID));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                    reader.Close();
                    return id;
                }
                reader.Close();
                return 0; //žádná Country s danným názvem nenalezena
            }
        }

        public String GetNameByContinent(int ContinentId)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            // using (SqlCommand command = new SqlCommand("SELECT * FROM Country AS c INNER JOIN Continent AS CO ON c.Continent_ID = CO.ID", conn))

            using (SqlCommand command = new SqlCommand("SELECT * FROM Country AS c WHERE c.ID = (SELECT TOP 1 ID FROM Country WHERE continent_id = @ContinentIdX ORDER BY NEWID())", conn))
            {
                command.Parameters.Add(new SqlParameter("@ContinentIdX", ContinentId));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String name = Convert.ToString(reader["Name"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                    reader.Close();
                    return name;
                }
                reader.Close();
                throw new Exception("there is no country in this continent" );
                //return ""; //žádná country nenaleyena  Bolí mne hlava
            }
        }

        public int GetId(string CountryName)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT ID FROM Country WHERE Country.Name = @NameX", conn))
            {
                command.Parameters.Add(new SqlParameter("@NameX", CountryName));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                    reader.Close();
                    return id;
                }
                reader.Close();
                throw new Exception("there is no country with this name: " + CountryName);
            }
        }


    }
}