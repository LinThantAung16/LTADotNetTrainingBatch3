namespace LTADotNetTrainingBatch3.WebAPI.Services
{
    public interface IProductADOService
    {
        ProductGetResponseDto GetProducts(int pageNo, int pageSize);

        //what to do
        ProductResponseDto CreateProduct(ProductCreateRequestDto request);
        //ProductResponseDto DeleteProduct(int id);
        //ProductGetByIdResponseDto GetProduct(int id);
        //ProductResponseDto PatchProduct(int id, ProductPatchRequestDto request);
        //ProductResponseDto UpdateProduct(int id, ProductUpdateRequestDto request);
    }
}