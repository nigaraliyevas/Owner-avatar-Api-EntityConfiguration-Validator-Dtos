namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.CategoryDto
{
    public class CategoryReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
