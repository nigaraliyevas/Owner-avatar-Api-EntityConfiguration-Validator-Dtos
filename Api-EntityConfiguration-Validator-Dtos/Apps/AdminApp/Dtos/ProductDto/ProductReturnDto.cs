namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.ProductDto
{
    public class ProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ProductCategoryReturnDto Category { get; set; }
    }
    public class ProductCategoryReturnDto
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
