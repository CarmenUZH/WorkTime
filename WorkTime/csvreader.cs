using System.Data.SqlClient;

namespace WorkTime
{
    public class csvreader
    {
        public void readcsv()
        {
            var lineNumber = 0;
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Temporary;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                conn.Open();
                using (StreamReader sr = new StreamReader(@"C:\Users\carmenkirchdorfer\Documents\GitHub\WorkTime\WorkTime\wwwroot\actualworktimes.csv"))
                {

                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (lineNumber != 0 && line.Length > 0)
                        {
                            var values = line.Split(';');
                            var sql = "INSERT INTO Worktime.dbo.Work VALUES ('" + values[0] + "','" + values[1] + "','" + values[2] + "','" + values[3] + "','" + values[4] + "','" + values[5] + "','" + values[6] + "')";
                            //var sql = "INSERT INTO Temporary.dbo.Test VALUES (2,'start','ende','startL','endeL','notizen','zahl1','zahl2','zahl3','zahl4')";
                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();

                        }
                        lineNumber++;

                    }
                    conn.Close();
                }
                Console.WriteLine("Import complete");
            }
        }
    }
}
