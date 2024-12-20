using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;





namespace ProducentConsumerProblem
{
    public class ContinentDAO
    {
        public IEnumerable<Continent> GetAll()
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Continent", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Continent continent = new Continent(
                        Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString()
                    );
                    yield return continent;
                }
                reader.Close();
            }
        }

        public void Save(Continent c)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            using (command = new SqlCommand("insert into Continent (name) values (@name)", conn))
            {
                command.Parameters.Add(new SqlParameter("@name", c.Name));
                
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int c)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM Continent WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", c));

                command.ExecuteNonQuery();
            }
        }

        public void Update(Continent c)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            SqlCommand command = null;

            using (command = new SqlCommand("update Continent set name = @name " + "where id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", c.Id));
                command.Parameters.Add(new SqlParameter("@name", c.Name));

                command.ExecuteNonQuery();
            }
        }

        public int GetId(string ContinentName)
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT ID FROM Continent WHERE Continent.Name = @NameX", conn))
            {
                command.Parameters.Add(new SqlParameter("@NameX", ContinentName));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                    reader.Close();
                    return id;
                }
                reader.Close();
                throw new Exception("there is no continent with this name: " + ContinentName);
            }
        }

        public int GetIdCount() //dá mi poèet id
        {
            SqlConnection conn = DatabaseSingleton.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) as CountX FROM Continent ", conn))
            {
                int count = 0;
                SqlDataReader reader = command.ExecuteReader();
       

                while (reader.Read())
                {
                    count = Convert.ToInt32(reader["CountX"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                }
                reader.Close();
                return count;

                if (count <= 0)
                {
                    throw new Exception("there is no continent in Database");
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

            using (SqlCommand command = new SqlCommand("SELECT * FROM Continent AS c WHERE c.ID = (SELECT TOP 1 ID FROM Continent ORDER BY NEWID())", conn))
            {

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);  //pokud ti to nefunguje tak máš tady chbu ty debile
                    reader.Close();
                    return id;
                }
                reader.Close();
                throw new Exception("there is no continent in Database");
            }
        }

    }
}
