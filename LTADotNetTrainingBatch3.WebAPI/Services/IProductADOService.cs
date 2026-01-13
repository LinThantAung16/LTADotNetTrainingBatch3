namespace LTADotNetTrainingBatch3.WebAPI.Services
{
    public interface IProductADOService
    {
        ProductGetResponseDto GetProducts(int pageNo, int pageSize);
    }
}