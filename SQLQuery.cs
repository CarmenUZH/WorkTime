using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data.SqlClient;


public class SQLQuery
{
	public CreateDatabase()
	{
		var connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		var script = File.ReadAllText(@"C:\Users\carmenkirchdorfer\Documents\GitHub\ALSOVerrechnug\ALSODatabase.sql");
		SqlConnection conn = new SqlConnection(connectionstring);
		Server server = new Server(new ServerConnection(conn));
		server.ConnectionContext.ExecuteNonQuery(script);
	
	
	}
}
