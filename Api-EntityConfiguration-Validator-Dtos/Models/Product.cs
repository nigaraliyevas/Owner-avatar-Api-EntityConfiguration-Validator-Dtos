using Api_EntityConfiguration_Validator_Dtos.Models.Common;

namespace Api_EntityConfiguration_Validator_Dtos.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsDelete { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
