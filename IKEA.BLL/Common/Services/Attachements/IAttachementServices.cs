using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.Attachements
{
    public interface IAttachementServices
    {
        public string UploadImage(IFormFile file, string folderName);
        public bool DeleteImage(string filePath);
    }
}
