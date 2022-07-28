
using Models;
using System.Data.SqlClient;

namespace Data
{
    public class Database : IDatacollector
    {
        private readonly SqlConnection _connection;
        SqlDataReader dataReader;
        private readonly List<Day> workdays = new List<Day>();
        private readonly string _datatable;


        public Database(string datatable)
        {
            _datatable = datatable;
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Worktime;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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

                var cmd = ExecuteCommand("SELECT * FROM " + _datatable); //Worktime.dbo.Work
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
            catch (NullReferenceException ex)
            {
                throw new ArgumentOutOfRangeException("Couldnt Access Data ", ex);

            }

            Console.WriteLine("complete");
            return workdays;
        }

        public void Add(Day day)
        {
            try
            {
                _connection.Open();
                var cmd = ExecuteCommand("INSERT INTO " + _datatable + " VALUES('" + day.Date + "', '" + day.Workstart + "', '" + day.Workend + "', '" + day.Lunchstart + "', '" + day.Lunchend + "', '" + day.Worktime + "', '" + day.Lunchworktime + "')");
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Couldnt Add ", ex);

            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Detected null value", ex);
            }

        }

        public int Commit()
        {
            return 0;
        }

        public void delete_withCondition(string condition)
        {

            try
            {
                _connection.Open();
                var cmd = ExecuteCommand("DELETE FROM " + _datatable + " WHERE " + condition);
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Couldnt Delete ", ex);

            }
        }
    }
}

