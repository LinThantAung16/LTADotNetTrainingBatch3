using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
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
        public ProductGetByIdResponseDto GetProduct(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            if (id <= 0)
            {
                return new ProductGetByIdResponseDto()
                {
                    IsSuccess = false,
                    Message = "Invalid product id",
                };
            }

            List<ProductDto> lts = new List<ProductDto>();
            string query = @"
                            select ProductId, ProductName, Quantity, Price from Tbl_Product
                            where ProductId = @ProductId and DeleteFlag = 0";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProductId", id);
            SqlDataReader reader = cmd.ExecuteReader();
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
            connection.Close();
            return new ProductGetByIdResponseDto()
            {
                IsSuccess = true,
                Message = "Success",
                Products = lts
            };
        }
        public ProductResponseDto CreateProduct(ProductCreateRequestDto product)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            if (product.ProductName == "" || product.Quantity == 0 || product.Price == 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "Need to fill every field"
                };
            }
            string query = @"
                            insert into Tbl_Product(ProductName, Quantity, Price,DeleteFlag,CreatedDateTime) values 
                            (@ProductName, @Quantity, @Price, 0, @dateTime )";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            if (result < 0) { 
            return new ProductResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to create product"
            };
            }

            return new ProductResponseDto()
            {
                IsSuccess = true,
                Message = "Product created successfully"
            };
            
        }

        public ProductResponseDto PatchProduct(int id, ProductPatchRequestDto request)
        {
            string conditions = "";
            if (!string.IsNullOrWhiteSpace(request.ProductName))
                conditions += "ProductName = @ProductName,";
            if (request.Quantity is not null && request.Quantity > 0)
                conditions += "Quantity = @Quantity,";
            if (request.Price is not null && request.Price > 0)
                conditions += "Price = @Price,";
            conditions += "ModifiedDateTime = @ModifiedDateTime";

            if (conditions.Length == 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "Invalid request.",
                };
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"
            UPDATE Tbl_Product SET {conditions}
        WHERE ProductId = @ProductId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ProductId", id);

            if (!string.IsNullOrWhiteSpace(request.ProductName))
                cmd.Parameters.AddWithValue("@ProductName", request.ProductName);
            if (request.Quantity is not null && request.Quantity > 0)
                cmd.Parameters.AddWithValue("@Quantity", request.Quantity);
            if (request.Price is not null && request.Price > 0)
                cmd.Parameters.AddWithValue("@Price", request.Price);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            if (result > 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = true,
                    Message = "Product updated successfully."
                };
            }

            return new ProductResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to update product.",
            };
        }

        public ProductResponseDto UpdateProduct(int id, ProductUpdateRequestDto request)
        {
            if(id <= 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "ProductID not Found"
                };
            }
            if(request.ProductName.IsNullOrEmpty()|| request.Quantity <= 0 || request.Price <= 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "All fields are required."
                };
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"Update Tbl_Product 
                             Set ProductName =@productName,
                                 Quantity = @quantity,
                                 Price = @price,
                                 ModifiedDateTime = @dateTime
                             Where ProductID = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@productName", request.ProductName);
            cmd.Parameters.AddWithValue("@quantity", request.Quantity);
            cmd.Parameters.AddWithValue("@price", request.Price);
            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
            var result = cmd.ExecuteNonQuery();
            connection.Close();
            if (result < 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "Update Fail"
                };
            }

            return new ProductResponseDto()
            {
                IsSuccess = true,
                Message = "Product Update Success"
            };

        }

        public ProductResponseDto DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "Product id not found"
                };
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"Update Tbl_Product
                             Set DeleteFlag = 1,
                                 ModifiedDateTime = @dateTime
                             Where ProductID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
            var result = cmd.ExecuteNonQuery();
            if(result < 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = false,
                    Message = "Delete failed"
                };
            }
            return new ProductResponseDto()
            {
                IsSuccess = true,
                Message = "Product Delete Success"
            };
        }

    }
}
