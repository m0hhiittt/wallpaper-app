using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperApi.Data.DTO.Creator
{
    public class CreateUserImageDTO
    {
        public int Id { get; set; }
        public IFormFile? ImagePath { get; set; }
    }
}
