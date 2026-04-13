namespace WallpaperApi.Data.DTO.Resolutions
{
    public class ResolutionDTO
    {
        public int Id { get; set; }
        public string? Resolution720P { get; set; }
        public string? Resolution1080P { get; set; }
        public string? Resolution2K { get; set; }
        public string? Resolution4K { get; set; }
        public string? Thumbnail { get; set; }
        public int? WallpaperId { get; set; }
        public int? ReviewProcessId { get; set; }
    }
}
