namespace LTADotNetTrainingBatch3.WebAPI.Services
{
    public class ProductService
    {
    }


    public class ProductCreateRequestDto
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductUpdateRequestDto
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductPatchRequestDto
    {
        public string? ProductName { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
    public class ProductResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ProductGetResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ProductDto> Products { get; set; }
    }
    

    public class ProductGetListResponseDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public bool DeleteFlag { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
