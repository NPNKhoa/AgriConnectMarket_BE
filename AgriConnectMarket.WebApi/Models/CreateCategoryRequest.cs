namespace AgriConnectMarket.WebApi.Models
{
    public class CreateCategoryRequest
    {
        public string CategortName { get; set; }
        public string CategoryDesc { get; set; }

        public IFormFile IllustractiveImage { get; set; }
    }
}
