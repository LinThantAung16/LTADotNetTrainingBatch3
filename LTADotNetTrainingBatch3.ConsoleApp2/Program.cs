// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "Batch3MiniPOS";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
connection.Open();

string query = @"SELECT [ProductID]
        ,[ProductName]
        ,[Quantity]
        ,[Price]
        ,[DeleteFlag]
        FROM [dbo].[TBL_Product]";

SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

connection.Close();

for(int i=0; i < dt.Rows.Count; i++)
{
    DataRow row = dt.Rows[i];
    //Console.WriteLine(row["ProductID"]);
    int rowNo = i + 1;
    decimal price = Convert.ToDecimal(row["Price"]);
    Console.WriteLine(rowNo.ToString() +  "." + row["ProductName"] +  "("  + price.ToString("n0") + ")" );
    //Console.WriteLine(row["Quantity"]);
    //Console.WriteLine("Price =>" + row["Price"]);
    //Console.WriteLine("---------------------------------");
}