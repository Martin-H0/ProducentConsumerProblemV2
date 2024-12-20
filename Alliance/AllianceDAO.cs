using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ProducentConsumerProblem
{
    public class AllianceDAO
    {
        public IEnumerable<Alliance> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Alliance", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Alliance alliance = new Alliance(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString()
                    );
                    yield return alliance;
                }
                reader.Close();
            }
        }

        public void Save(Alliance a)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            using (command = new SqlCommand("insert into Alliance (name, type) values (@name, @type)", conn))
            {
                command.Parameters.Add(new SqlParameter("@name", a.Name));
                command.Parameters.Add(new SqlParameter("@type", a.Type));
                
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int a)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Alliance WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", a));

                command.ExecuteNonQuery();
            }
        }

        public void Update(Alliance a)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            using (command = new SqlCommand("update Alliance set name = @name, type = @type " + "where id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", a.Id));
                command.Parameters.Add(new SqlParameter("@name", a.Name));
                command.Parameters.Add(new SqlParameter("@type", a.Type));

                command.ExecuteNonQuery();
            }
        }


        public int GetId(string AllianceName)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT ID FROM Alliance WHERE Alliance.Name = @NameX", conn))
            {
                command.Parameters.Add(new SqlParameter("@NameX", AllianceName));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader[0].ToString());
                    reader.Close();
                    return id;
                }
                reader.Close();
                throw new Exception("there is no alliance with this name: " + AllianceName);
            }
        }

        public int GetIdCount() //dá mi poèet id
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) as CountX FROM Alliance ", conn))
            {
                SqlDataReader reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count = Convert.ToInt32(reader["CountX"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                }
                reader.Close();
                if (count <= 0)
                {
                    throw new Exception("there is no alliance in Database");
                }
                else
                {
                    return count;
                }
            }
        }

        public int GetRandomID()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            // using (SqlCommand command = new SqlCommand("SELECT * FROM Country AS c INNER JOIN Continent AS CO ON c.Continent_ID = CO.ID", conn))

            using (SqlCommand command = new SqlCommand("SELECT * FROM Alliance AS c WHERE c.ID = (SELECT TOP 1 ID FROM Alliance ORDER BY NEWID())", conn))
            {

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                    reader.Close();
                    return id;
                }
                reader.Close();
                throw new Exception("there is no country in this Alliance");
            }
        }




    }
}