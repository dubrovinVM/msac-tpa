using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace msac_tpa_new.BusinessLogic
{
    public class FileHandler
    {
        public static async Task<string> SaveAvatarAsync(IFormFile file, string surname, string imageFolder)
        {
            try
            {
                if (file != null && file.Length != 0)
                {
                    var extenstion = Path.GetExtension(file.FileName);
                    var latSurname = Transliterate.Translit(surname);
                    var fileNewName = string.Format($"{latSurname}{extenstion}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{imageFolder}", fileNewName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return fileNewName;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void RemoveAvatar(string fileName, string imageFolder)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(imageFolder))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"{imageFolder}", fileName);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
