namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.CategoryDto
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public IFormFile ImgaeURLUpload { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
