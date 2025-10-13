using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallpaperApi.Data.Model;
using WallpaperApi.DTO.Wallpaper;
using WallpaperApi.Services.Mapper;
using WallpaperApi_Model;

namespace Review.Services.Mapper
{
    public static class ReviewProcessMapper
    {
        public static ReviewDto ToReviewDto(this ReviewProcess model)
        {
            return new ReviewDto
            {
                Id = model.Id,
                Title = model.Title,
                FilePath = model.FilePath,
                WallpaperTag = model.WallpaperTag,
                Extention = model.Extention,
                Status = model.Status,
                CreatorId = model.CreatorId,
                Resolutions = model.Resolutions.Select(ResolutionMapper.ToResolutionMapper).ToList(),

            };
        }

        public static ReviewProcess ToReviewFromCreate(this CreateReviewDto model, string path, string ext)
        {
            return new ReviewProcess
            {
                CreatorId = model.CreatorId,   
                Title = model.Title,
                FilePath = path,
                Extention = ext,
            };
        }

        public static ReviewProcess ToReviewFromUpdate(this UpdateReviewDto model, string path)
        {
            return new ReviewProcess
            {
               Id=model.Id,
               Status= model.Action
            };
        }
    }
}
