using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Linq; //Important for SQL query operations

namespace Data
{
    public class SQLQuery : ISQLQuery
    {

        public void CreateDatabase()
        {
            var connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string script = File.ReadAllText(@"C:\Users\carmenkirchdorfer\Documents\GitHub\ALSOVerrechnug\TESTDatabase.sql");
            Console.WriteLine(script);
            SqlConnection conn = new SqlConnection(connectionstring);
            ServerConnection connection = new ServerConnection(conn);
            Server server = new Server(connection);
            server.ConnectionContext.ExecuteNonQuery(script);


        }

        public void CreatePostGres()
        {
            var connString = "Host=10.20.101.211;Username=ocsuser;Password=rx5bRIg$XqzrNlp3kzlRt;Database=postgres"; //Even though we are creating a new database you have to specify that its a postgres database

            var conn = new NpgsqlConnection(connString);
            conn.Open();

            // Insert some data
            using (var cmd = new NpgsqlCommand(@"CREATE DATABASE sqltest WITH OWNER = ocsuser ENCODING = 'UTF8' CONNECTION LIMIT = -1;", conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }
    }
}