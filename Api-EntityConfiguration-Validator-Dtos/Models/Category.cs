using Api_EntityConfiguration_Validator_Dtos.Models.Common;

namespace Api_EntityConfiguration_Validator_Dtos.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public bool IsDelete { get; set; }
        public List<Product> Products { get; set; }
    }
}
