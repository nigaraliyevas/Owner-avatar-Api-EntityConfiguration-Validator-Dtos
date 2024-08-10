namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.ProductDto
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsDelete { get; set; }
        public int CategoryId { get; set; }
        public ProductCategoryReturnDto Category { get; set; }
    }
}
