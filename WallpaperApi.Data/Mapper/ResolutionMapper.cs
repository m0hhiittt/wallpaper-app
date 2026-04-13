using WallpaperApi.Data.DTO.Resolutions;
using WallpaperApi.Data.Model;

namespace WallpaperApi.Services.Mapper
{
    public static class ResolutionMapper
    {
        public static ResolutionDTO ToResolutionMapper(Resolutions resolution)
        {
            return new ResolutionDTO
            {
                Id = resolution.Id,
                Resolution720P = resolution.Resolution720P, 
                Resolution1080P = resolution.Resolution1080P,
                Resolution2K = resolution.Resolution2K,
                Resolution4K = resolution.Resolution4K,
                ReviewProcessId = resolution.ReviewProcessId,
                Thumbnail = resolution.Thumbnail,
                WallpaperId = resolution.WallpaperId,
            };
        }

        //public static ResolutionDTO ToResolutionMapper(Resolutions resolution, string baseUrl)
        //{
        //    return new ResolutionDTO
        //    {
        //        Id = resolution.Id,
        //        Resolution720P = $"{baseUrl}/{resolution.Resolution720P}",
        //        Resolution1080P = $"{baseUrl}/{resolution.Resolution1080P}",
        //        Resolution2K = $"{baseUrl}/{resolution.Resolution2K}",
        //        Resolution4K = $"{baseUrl}/{resolution.Resolution4K}",
        //        Thumbnail = $"{baseUrl}/{resolution.Thumbnail}",
        //        WallpaperId = resolution.WallpaperId,
        //        ReviewProcessId = resolution.ReviewProcessId,
        //    };
        //}

        public static Resolutions ToCreateFromResolution(CreateResolutionDto resolution)
        {
            return new Resolutions
            {
                Id = resolution.Id,
                Resolution720P = resolution.Resolution720P,
                Resolution1080P = resolution.Resolution1080P,
                Resolution2K = resolution.Resolution2K,
                Resolution4K = resolution.Resolution4K,
                ReviewProcessId = resolution.ReviewProcessId,
                Thumbnail = resolution.Thumbnail,
            };
        }

        //public static void ToUpdateFromResolution(UpdateResolutionDTO resolution, Resolutions entity)
        //{
        //    resolution.Resolution720P = entity.Resolution720P;
        //    resolution.Resolution1080P= entity.Resolution1080P;
        //    resolution.Resolution2K = entity.Resolution2K;
        //    resolution.Resolution4K = entity.Resolution4K;
        //    resolution.WallpaperId = entity.WallpaperId;
        //}
    }
}
