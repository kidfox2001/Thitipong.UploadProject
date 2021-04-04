using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Thitipong.Upload.Service
{
   public class FileService
    {

        public string Upload(IFormFile file)
        {
            var uploadDirecotroy =  "Uploads/";
            var uploadPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), uploadDirecotroy);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);


            using (var strem = File.Create(filePath))
            {
                file.CopyTo(strem);
            }
            return filePath;
        }

    }
}
