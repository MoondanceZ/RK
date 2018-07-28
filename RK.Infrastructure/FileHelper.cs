using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RK.Infrastructure
{
    public class FileHelper
    {
        const string IMAGE_FILE_PATH = @"file\image\";
        public static async Task<string> DownAsync(string fileUrl, string fileExtension = "")
        {
            if (fileExtension == "")
                fileExtension = Path.GetExtension(fileUrl);
            fileExtension = fileExtension.StartsWith(".") ? fileExtension : "." + fileExtension;
            var roorDir = AppContext.BaseDirectory + "wwwroot\\";
            var filePath = roorDir + Path.Combine(IMAGE_FILE_PATH, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + fileExtension;
            var fileFullPath = filePath + "\\" + fileName;
            var returnFilePath = "/" + (Path.Combine(IMAGE_FILE_PATH, DateTime.Now.ToString("yyyy-MM-dd")) + "/" + fileName).Replace("\\", "/");
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStreamAsync(fileUrl);
                using (StreamWriter sw = new StreamWriter(fileFullPath))
                {
                    result.CopyTo(sw.BaseStream);
                    sw.Flush();
                    sw.Close();
                }
            }
            return returnFilePath;
        }
    }
}
