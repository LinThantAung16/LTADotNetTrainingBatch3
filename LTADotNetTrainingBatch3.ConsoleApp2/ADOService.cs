using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTADotNetTrainingBatch3.ConsoleApp2
{
    public class ADOService
    {
        SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DELL-16-LIN\\MSSQLSERVER01",
            InitialCatalog = "Batch3MiniPOS",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        public void ReadProduct()
        {
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.DataSource = ".";
            //sqlConnectionStringBuilder.InitialCatalog = "Batch3MiniPOS";
            //sqlConnectionStringBuilder.UserID = "sa";
            //sqlConnectionStringBuilder.Password = "sasa@123";
            //sqlConnectionStringBuilder.TrustServerCertificate = true;

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
            adapter.Fill(dt); //execute query and fill data to datatable

            connection.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                //Console.WriteLine(row["ProductID"]);
                int rowNo = i + 1;
                decimal price = Convert.ToDecimal(row["Price"]);
                Console.WriteLine( "Product ID :" + row["ProductID"] + "  " + row["ProductName"] + " |quantity:"+ row["Quantity"] + " (" + price.ToString("n0") + ")");
                //Console.WriteLine(row["Quantity"]);
                //Console.WriteLine("Price =>" + row["Price"]);
                //Console.WriteLine("---------------------------------");
            }
        }

        public void CreateProduct(string productName, int quantity, decimal price)
        {
            string query = @"INSERT INTO [dbo].[TBL_Product]
           ([ProductName]
           ,[Quantity]
           ,[Price]
           ,[DeleteFlag]
           ,[CreatedDateTime])
           Values
           ('" + productName + @"'
           ,'" + quantity + @"'
           ,'" + price + @"'
           ,0
            ,'" + DateTime.Now + "')";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            Console.WriteLine(message);
        }

       
        public void UpdateProduct(int productID, string productName, int quantity, decimal price)
        {
            string query = @" UPDATE[dbo].[TBL_Product]
                            SET[ProductName] = '" + productName+@"'
                          ,[Quantity] = '"+quantity+ @"'
                          ,[Price] = '"+price+@"'
                          ,[ModifiedDateTime] = '"+DateTime.Now+@"'
                            WHERE ProductID = '"+productID+"' ";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Updated Successful" : "Update Failed";
            Console.WriteLine(message);
        }


        public void DeleteProduct(int productID)
        {
            string query = "Delete FROM [dbo].[TBL_Product] where ProductID = '"+productID+"' ";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Delete Successful" : "Delete Failed. Invalid product ID";
            Console.WriteLine(message);

        }

        decimal productPrice;
        public bool checkProductQuantity(int productID, int quantity)
        {
            string query = "SELECT * FROM [dbo].[TBL_Product] where ProductID = '" + productID + "' ";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int qty = Convert.ToInt32(reader["Quantity"]);
                productPrice = Convert.ToDecimal(reader["Price"]);
              if(qty >= quantity)
              {
                   connection.Close();
                    return true;
               }
                connection.Close();
                return false;
                
            }
            return false;
        }

        public void ReadSale()
        {
           
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"SELECT[SaleId]
                          ,[ProductId]
                          ,[Quantity]
                          ,[Price]
                          ,[DeleteFlag]
                          ,[CreatedDateTime]
                          ,[ModifiedDateTime]
                            FROM[dbo].[TBL_Sale]";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                //Console.WriteLine(row["ProductID"]);
                int rowNo = i + 1;
                decimal price = Convert.ToDecimal(row["Price"]);
                Console.WriteLine(rowNo + "." + "Product ID " + row["ProductId"] + " | Quantity:" + row["Quantity"] + " (" + price.ToString("n0") + ")");
                
            }
        }
        public void CreateSale(int productID, int quantity)
        {
           

            string query = @"INSERT INTO [dbo].[TBL_Sale]
           ([ProductID]
           ,[Quantity]
           ,[Price]
           ,[DeleteFlag]
           ,[CreatedDateTime])
           Values
           ('"+productID+ @"'
           ,'"+quantity+ @"'
           ,'"+productPrice+ @"'
           ,0
           ,'"+DateTime.Now+"')";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            int result2 = cmd.ExecuteNonQuery();
            string message = result2 > 0 ? "Saving Successful" : "Saving Failed";


            // Update product quantity
            string updateQuery = @"UPDATE [dbo].[TBL_Product]
                                   SET [Quantity] = [Quantity] - " + quantity + @"
                                     ,[ModifiedDateTime] = '" + DateTime.Now + @"'
                                   WHERE [ProductID] = " + productID;



            SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
            int result = updateCmd.ExecuteNonQuery();
            connection.Close();
         
            Console.WriteLine(message);
        }
    }
}
