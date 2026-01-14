namespace LTADotNetTrainingBatch3.WebAPI.Services
{
    public interface IProductADOService
    {
        ProductGetResponseDto GetProducts(int pageNo, int pageSize);
        ProductGetByIdResponseDto GetProduct(int id);
        ProductResponseDto CreateProduct(ProductCreateRequestDto request);
        ProductResponseDto PatchProduct(int id, ProductPatchRequestDto request);
        ProductResponseDto UpdateProduct(int id, ProductUpdateRequestDto request);
        ProductResponseDto DeleteProduct(int id);
    }
}