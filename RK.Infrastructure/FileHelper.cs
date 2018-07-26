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
        const string IMAGE_FILE_PATH = @"File\Image\";
        public static async Task<string> DownAsync(string fileUrl, string fileExtension = "")
        {
            if (fileExtension == "")
                fileExtension = Path.GetExtension(fileUrl);
            fileExtension = fileExtension.StartsWith(".") ? fileExtension : "." + fileExtension;
            var roorDir = AppContext.BaseDirectory;
            var filePath = roorDir + Path.Combine(IMAGE_FILE_PATH, DateTime.Now.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var fileFullPath = filePath + "\\" + Guid.NewGuid().ToString().Replace("-", "") + fileExtension;

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
            return fileFullPath;
        }
    }
}
