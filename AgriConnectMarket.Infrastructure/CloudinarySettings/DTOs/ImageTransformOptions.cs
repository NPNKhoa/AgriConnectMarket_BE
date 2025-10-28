namespace AgriConnectMarket.Infrastructure.CloudinarySettings.DTOs
{
    public class ImageTransformOptions
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Crop { get; set; }    // e.g. "fill", "fit", "thumb"
        public string? Gravity { get; set; } // e.g. "auto", "face"
        public int? Quality { get; set; }    // e.g. 80
        public string? Format { get; set; }  // e.g. "jpg", "webp"
    }

}
