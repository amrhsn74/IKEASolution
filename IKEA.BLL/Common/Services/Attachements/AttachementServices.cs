using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.Attachements
{
    public class AttachementServices : IAttachementServices
    {
        private readonly List<string> _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        private const int _maxFileSize = 5 * 1024 * 1024; // 5 MB

        public string UploadImage(IFormFile file, string folderName)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            
            if (!_allowedExtensions.Contains(fileExtension))
                throw new Exception("Invalid file type. Allowed types are: " + string.Join(", ", _allowedExtensions));
            
            if (file.Length > _maxFileSize)
                throw new Exception("File size exceeds the maximum limit of 5 MB.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);
            
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);
            
            file.CopyTo(fs);

            return fileName;
        }
        
        public bool DeleteImage(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    
    }
}
