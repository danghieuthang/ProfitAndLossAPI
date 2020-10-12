using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.Helpers
{
    public  class FileHelper
    {
        public static bool IsImageFile(string fileName)
        {
            var extension = "." + fileName.Split('.')[fileName.Split('.').Length - 1];
            return (extension == ".png" || extension == ".jpg" || extension == ".gif");
        }

    }
}
