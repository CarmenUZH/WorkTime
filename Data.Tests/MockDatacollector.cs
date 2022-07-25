using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tests
{
    internal class MockDatacollector : IDatacollector
    {
        private readonly SqlConnection _connection;
        private readonly List<Day> mockdays = new List<Day>();
        SqlDataReader dataReader;


        public MockDatacollector()
        {
            //For the future: Figure out how to get this to run from remote (Github Actions)
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Temporary;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        private SqlCommand ExecuteCommand(string sql)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = _connection;
            Console.WriteLine(sql);
            return cmd;
        }
        public void Add(Day day)
        {

            try
            {
                _connection.Open();
                var cmd = ExecuteCommand("INSERT INTO Temporary.dbo.TestDays VALUES('" + day.Date + "', '" + day.Workstart + "', '" + day.Workend + "', '" + day.Lunchstart + "', '" + day.Lunchend + "', '" + day.Worktime + "', '" + day.Lunchworktime + "')");
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine("Couldn't add day");
            }
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Day> getData()
        {

            if (mockdays.Count > 0)
            {
                mockdays.Clear();
            }
            try
            {

                _connection.Open();

                var cmd = ExecuteCommand("SELECT * FROM Temporary.dbo.TestDays");
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    mockdays.Add(new Day()
                    {
                        Date = dataReader["one"].ToString(),
                        Workstart = dataReader["two"].ToString(),
                        Workend = dataReader["three"].ToString(),
                        Lunchstart = dataReader["four"].ToString(),
                        Lunchend = dataReader["five"].ToString(),
                        Worktime = dataReader["six"].ToString(),
                        Lunchworktime = dataReader["seven"].ToString()
                    });
                }
                _connection.Close();

            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine("Oops, something went wrong while getting the data");
            }

  
            return mockdays;
        }

        public void delete_withCondition(string condition)
        {

            try
            {
                _connection.Open();
                var cmd = ExecuteCommand("DELETE FROM Temporary.dbo.TestDays WHERE " + condition);
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine("Couldn't delete day");
            }
        }
    }
}
