using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Helper
{
    public static class FileUploader
    {
        public static string Upload(string FolderName, IFormFile File)
        {

            try
            {
                // 1 ) Get Directory
                string FolderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Docs\" + FolderName;


                //2) Get File Name
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);


                // 3) Merge Path with File Name
                string FinalPath = Path.Combine(FolderPath, FileName);


                //4) Save File As Streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }

                return FileName;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static void RemoveFile(string FolderName, string FileName)
        {

            string file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Docs", FolderName, FileName);

            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

    }
}
