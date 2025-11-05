namespace AgriConnectMarket.WebApi.Models
{
    public class UpdateCategoryRequest
    {
        public string CategortName { get; set; }
        public string CategoryDesc { get; set; }

        public IFormFile IllustractiveImage { get; set; }
    }
}
