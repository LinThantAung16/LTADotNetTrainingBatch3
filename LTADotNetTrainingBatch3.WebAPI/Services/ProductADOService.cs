using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LTADotNetTrainingBatch3.WebAPI.Services
{
    //Crt + R, I to implement interface
    public class ProductADOService : IProductADOService
    {
        private readonly string _connectionString;
        public ProductADOService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }

        public ProductGetResponseDto GetProducts(int pageNo, int pageSize)
        {
            var lts = new List<ProductDto>();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            int skip = (pageNo - 1) * pageSize;
            string query = @"
            SELECT ProductId, ProductName, Quantity, Price FROM Tbl_Product
            WHERE DeleteFlag = 0
            ORDER BY ProductId DESC
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Skip", skip);
            sqlCommand.Parameters.AddWithValue("@Take", pageSize);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var item = new ProductDto()
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = Convert.ToString(reader["ProductName"])!,
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDecimal(reader["Price"])
                };

                lts.Add(item);
            }

            sqlConnection.Close();

            return new ProductGetResponseDto()
            {
                IsSuccess = true,
                Message = "Success.",
                Products = lts
            };
        }


    }
}
