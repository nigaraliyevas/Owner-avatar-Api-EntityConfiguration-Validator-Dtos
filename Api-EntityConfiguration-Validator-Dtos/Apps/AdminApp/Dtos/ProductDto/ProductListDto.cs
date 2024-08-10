namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.ProductDto
{
    public class ProductListDto
    {
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public List<ProductReturnDto> Items { get; set; }
    }
}
