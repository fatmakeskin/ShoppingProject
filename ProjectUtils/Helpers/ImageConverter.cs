using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ProjectUtils.Helpers
{
    public static class ImageConverter
    {
        public static string ImageConvert(this string imageBase64)
        {
            byte[] imageBytes = Encoding.UTF8.GetBytes(imageBase64);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
