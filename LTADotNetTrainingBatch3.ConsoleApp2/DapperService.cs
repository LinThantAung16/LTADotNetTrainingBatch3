using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTADotNetTrainingBatch3.ConsoleApp2
{
    public class DapperService
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
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                
                string query = @"SELECT [ProductID]
                            ,[ProductName]
                            ,[Quantity]
                            ,[Price]
                            ,[DeleteFlag]
                            FROM [dbo].[TBL_Product]";
                List<ProductDto> lst = db.Query<ProductDto>(query).ToList();
                for(int i = 0; i < lst.Count; i++)
                {
                    Console.WriteLine("Product ID: " + lst[i].ProductID + " Product Name:" + lst[i].ProductName + " Quantity: " + lst[i].Quantity + " Price: " + lst[i].Price );
                }
            }
           
        }
        public void CreateProduct(string productName , int quantity, decimal price)
        {
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
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
                int result = db.Execute(query);
                string message = result > 0 ? "Saving Successful" : "Saving Failed";
                Console.WriteLine(message);
            }
        }
        public void UpdateProduct(int productID, string productName, int quantity, decimal price)
        {
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                string query = @" UPDATE[dbo].[TBL_Product]
                            SET[ProductName] = '" + productName + @"'
                          ,[Quantity] = '" + quantity + @"'
                          ,[Price] = '" + price + @"'
                          ,[ModifiedDateTime] = '" + DateTime.Now + @"'
                            WHERE ProductID = '" + productID + "' ";   
                int result = db.Execute(query);
                string message = result > 0 ? "Update Successful" : "Update Failed";
            }
        }

        public void DeleteProduct(int productID)
        {
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                string query = "Delete FROM [dbo].[TBL_Product] where ProductID = '" + productID + "' ";
                int result = db.Execute(query);
                string message = result > 0 ? "Delete Successful" : "Delete Failed";
            }
        }

        decimal productPrice;
        public bool checkQuantity(int productID, int quantity)
        {
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                string query = "SELECT * FROM [dbo].[TBL_Product] where ProductID = '" + productID + "' ";
                List<ProductDto> lst = db.Query<ProductDto>(query).ToList();
                if (lst.Count > 0)
                {
                    if (lst[0].Quantity >= quantity)
                    {
                        productPrice = lst[0].Price;
                        return true;
                    }
                    
                }
                return false;

            }
        }
        public void ReadSale()
        {
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                string query = @"SELECT[SaleId]
                          ,[ProductId]
                          ,[Quantity]
                          ,[Price]
                          ,[DeleteFlag]
                          ,[CreatedDateTime]
                          ,[ModifiedDateTime]
                            FROM[dbo].[TBL_Sale]";
                List<SaleDto> lst = db.Query<SaleDto>(query).ToList();
                for (int i = 0; i < lst.Count; i++)
                {
                    Console.WriteLine(" Product ID:" + lst[i].ProductID + " Quantity: " + lst[i].Quantity + " Price: " + lst[i].Price + " Created DateTime: " + lst[i].CreatedDateTime);
                }
            }
        }
        public void CreateSale(int productID, int quantity)
        {
            using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                db.Open();
                string query = @"INSERT INTO [dbo].[TBL_Sale]
           ([ProductID]
           ,[Quantity]
           ,[Price]
           ,[DeleteFlag]
           ,[CreatedDateTime])
           Values
           ('" + productID + @"'
           ,'" + quantity + @"'
           ,'" + productPrice + @"'
           ,0
           ,'" + DateTime.Now + "')";
                int result = db.Execute(query);
                string message = result > 0 ? "Sale Created Successfully" : "Sale Creation Failed";

                //update product quantity
                string updateQuery = @"UPDATE [dbo].[TBL_Product]
                                       SET [Quantity] = [Quantity] - " + quantity + @"
                                         ,[ModifiedDateTime] = '" + DateTime.Now + @"'
                                       WHERE [ProductID] = " + productID;
                db.Execute(updateQuery);
                Console.WriteLine(message);
            }
        }

    }

    public class ProductDto //Data Transfer Object
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool DeleteFlag { get; set; }
    }

    public class SaleDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
