using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Helpers
{
    public static class FileManager
    {
       

        public static string Save(string root, string folder, IFormFile file)
        {
            var guid = Guid.NewGuid().ToString();


            //string path = _env.WebRootPath+@"\upload\sliders\" +guid+slider.ImageFile.FileName.Substring(slider.ImageFile.FileName.Length-64,64);
            string path = Path.Combine(root, "upload/sliders", guid + (file.FileName.Length>64? file.FileName.Substring(file.FileName.Length - 64, 64):file.FileName));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
                return guid + (file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName);
        }
        public static bool Delete(string root, string folder, string filename)
        {
            string path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
        public static void DeleteAll(string root, string folder,List<string> fileNames)
        {
            foreach (var filename in fileNames)
            {
                string path = Path.Combine(root, folder, filename);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            
         
        }
    }
}
