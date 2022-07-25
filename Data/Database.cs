
using Models;
using System.Data.SqlClient;

namespace Data
{
    public class Database : IDatacollector
    {
        private readonly SqlConnection _connection;
        SqlDataReader dataReader;
        private readonly List<Day> workdays = new List<Day>();
        

        public Database()
        {
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

        public IEnumerable<Day> getData()
        {
            if (workdays.Count > 0)
            {
                workdays.Clear();
            }
            try
            {

                _connection.Open();

                var cmd = ExecuteCommand("SELECT * FROM Worktime.dbo.Work");
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    workdays.Add(new Day()
                    {
                        Date = dataReader["date"].ToString(),
                        Workstart = dataReader["workstart"].ToString(),
                        Workend = dataReader["workend"].ToString(),
                        Lunchstart = dataReader["lunchstart"].ToString(),
                        Lunchend = dataReader["lunchend"].ToString(),
                        Worktime = dataReader["worktime"].ToString(),
                        Lunchworktime = dataReader["lunchworktime"].ToString()
                    });
                }
                _connection.Close();
              
            }
            catch (Exception ex)
            {
                throw ex;
               // Console.WriteLine("Oops, something went wrong while getting the data");
            }

            Console.WriteLine("complete");
            return workdays;
        }

        public void Add(Day day)
        {
            Console.WriteLine("reached to heare");
            try
            {
                _connection.Open();
                var cmd = ExecuteCommand("INSERT INTO Worktime.dbo.Work VALUES('" + day.Date + "', '" + day.Workstart + "', '" + day.Workend + "', '" + day.Lunchstart + "', '" + day.Lunchend + "', '" + day.Worktime + "', '" + day.Lunchworktime + "')");
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                 //Console.WriteLine("Couldn't add day");
            }
           
        }

        public int Commit()
        {
            return 0;
        }
    }
}

