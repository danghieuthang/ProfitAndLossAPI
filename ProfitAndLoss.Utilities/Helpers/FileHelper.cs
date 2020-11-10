using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.Utilities.Helpers
{
    public static class FileHelper
    {
        public static bool IsImageFile(this string fileName)
        {
            var extension = "." + fileName.Split('.')[^1];
            return (extension == ".png" || extension == ".jpg" || extension == ".gif");
        }
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static MemoryStream GetMemoryStream(this IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyToAsync(memoryStream);
            return memoryStream;
        }

        public static FileStream GetFileStream(this IFormFile formFile)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
            var fileStream = new FileStream(Path.Combine(path, formFile.FileName), FileMode.Open);
            return fileStream;
        }

        public static string Extension(this IFormFile formFile)
        {
            var extension = "." + formFile.FileName.Split('.')[^1];
            return extension;
        }

    }
}
