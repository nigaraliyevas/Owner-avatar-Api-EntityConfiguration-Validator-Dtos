namespace ShopApp.MVC.ViewModels.Product
{
    public class ProductListDto
    {
        public int Page { get; set; }
        public int TotalCount { get; set; }
        public List<ProductListItemDto> Items { get; set; }
    }
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CategoryInProductReturnDto Category { get; set; }
    }
    public class CategoryInProductReturnDto
    {
        public string Name { get; set; }
        public int ProductsCount { get; set; }
    }
}
