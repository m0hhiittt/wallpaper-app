using Review.Services.Mapper;
using SixLabors.ImageSharp;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Interface.Repository;
using WallpaperApi.Services.Interface.Service;
using WallpaperApi.Services.Mapper;
using WallpaperApi.Services.Repository;
using WallpaperApi_Model.Model;

namespace WallpaperApi.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IWebHostEnvironment _env;
        private readonly IWallpaperRepository _wallpaperRepo;

        public ReviewService(IReviewRepository repository, IWebHostEnvironment env, IWallpaperRepository wallpaperRepo)
        {
            _repository = repository;
            _env = env;
            _wallpaperRepo = wallpaperRepo;
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto)
        {
            var contentPath = _env.ContentRootPath;
            var uploadPath = Path.Combine(contentPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var ext = Path.GetExtension(dto.File.FileName).ToLowerInvariant();
            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                throw new Exception("Invalid file type. Only JPG, JPEG, and PNG are allowed.");

            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // encoder (JPEG 75)
            var jpgEncoder = ImageCodecInfo.GetImageDecoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);

            string? thumbPath = null, thumbFileName = $"thumb_{fileName}", res720 = null, res1080 = null, res2k = null, res4k = null;

            using (var imageStream = new MemoryStream(await File.ReadAllBytesAsync(path)))
            using (var originalImage = System.Drawing.Image.FromStream(imageStream))
            {
                int width = originalImage.Width;
                int height = originalImage.Height;

                // calculate aspect ratio
                double aspectRatio = (double)width / height;
                string ratioLabel;
                if (Math.Abs(aspectRatio - 1.0) < 0.05)
                    ratioLabel = "1:1";   // square
                else if (aspectRatio > 1)
                    ratioLabel = "16:9";  // landscape
                else
                    ratioLabel = "9:16";  // portrait

                // ---- Thumbnail (always) ----
                thumbPath = Path.Combine(uploadPath, thumbFileName);
                ResizeAndSave(originalImage, width / 2, height / 2, thumbPath, jpgEncoder, encoderParams);
                

                // ---- Resolution presets ----
                var presets = new List<(int w, int h, string label)>
                {
                    (1280, 720, "720p"),
                    (1920, 1080, "1080p"),
                    (2560, 1440, "2k"),
                    (3840, 2160, "4k")
                };

                foreach (var (targetW, targetH, label) in presets)
                {
                    // scale depending on orientation
                    int finalW, finalH;
                    if (ratioLabel == "16:9") // landscape
                    {
                        finalW = targetW;
                        finalH = (int)(targetW / aspectRatio);
                    }
                    else if (ratioLabel == "9:16") // portrait
                    {
                        finalH = targetH;
                        finalW = (int)(targetH * aspectRatio);
                    }
                    else // square
                    {
                        finalW = targetW;
                        finalH = targetW; // keep square
                    }

                    // only resize if original is big enough
                    if (width >= finalW && height >= finalH)
                    {
                        var savePath = Path.Combine(uploadPath, $"{label}_{fileName}");
                        ResizeAndSave(originalImage, finalW, finalH, savePath, jpgEncoder, encoderParams);
                        var resolutionFileName = $"{label}_{fileName}";

                        switch (label)
                        {
                            case "720p": res720 = resolutionFileName; break;
                            case "1080p": res1080 = resolutionFileName; break;
                            case "2k": res2k = resolutionFileName; break;
                            case "4k": res4k = resolutionFileName; break;
                        }
                    }
                }
            }

            var review = dto.ToReviewFromCreate(fileName,ext);

            var resolutions = new Resolutions
            {
                Thumbnail = thumbFileName,
                Resolution720P = res720,
                Resolution1080P = res1080,
                Resolution2K = res2k,
                Resolution4K = res4k,
                ReviewProcess = review
            };

            review.Resolutions.Add(resolutions);

            var saved = await _repository.AddReviewAsync(review);
            return saved.ToReviewDto();
        }

        // helper
        private void ResizeAndSave(System.Drawing.Image original, int targetWidth, int targetHeight,
                                   string outputPath, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            using (var newImage = new Bitmap(targetWidth, targetHeight))
            using (var g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, targetWidth, targetHeight);
                newImage.Save(outputPath, encoder, encoderParams);
            }
        }




        public async Task<ReviewDto?> GetReviewByIdAsync(int id)
        {
            var review = await _repository.GetReviewByIdAsync(id);
            return review?.ToReviewDto();
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _repository.GetAllReviewsAsync();
            return reviews.Select(r => r.ToReviewDto());
        }

        public async Task<ReviewDto?> UpdateReviewStatusAsync(int id, string action)
        {
            var review = await _repository.UpdateReviewStatusAsync(id, action);

            if (review == null) return null;

            if (review.Status == "Approved")
            {
                var wallpaper = new Wallpaper
                {
                    Title = review.Title,
                    FilePath = review.FilePath,
                    WallpaperTag = review.WallpaperTag,
                    Extention = review.Extention,
                    CreatorId = review.CreatorId,
                    Resolutions = review.Resolutions.Select(r => new Resolutions
                    {
                        Resolution720P = r.Resolution720P,
                        Resolution1080P = r.Resolution1080P,
                        Resolution2K = r.Resolution2K,
                        Resolution4K = r.Resolution4K,
                        Thumbnail = r.Thumbnail
                    }).ToList()
                };

                await _wallpaperRepo.CreateWallpaperAsync(wallpaper);
            }

            return review.ToReviewDto();
        }
    }
}