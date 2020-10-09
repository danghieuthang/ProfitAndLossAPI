using OfficeOpenXml;
using OfficeOpenXml.Style;
using ProfitAndLoss.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProfitAndLoss.Business.Helpers
{
    public static class ExcelHelper<T>
    {
        private static readonly Color BackGRColor = Color.FromArgb(50, 82, 168);
        private static readonly Color TitleBGRColor = Color.FromArgb(91, 155, 213);

        /// <summary>
        /// Export list <typeparamref name="T"/> excel
        /// </summary>
        /// <param name="data">The list data export</param>
        /// <param name="sheetName">The sheet name</param>
        /// <returns></returns>
        public static byte[] Export(List<T> data, string sheetName)
        {
            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage(new MemoryStream()))
            {
                // Set some properties
                //excelPackage.Workbook.Properties.Author = "Thang Depzai";
                excelPackage.Workbook.Properties.Title = sheetName;

                // Add worksheet
                var workSheet = excelPackage.Workbook.Worksheets.Add(sheetName);

                var columns = data.FirstOrDefault().GetType().GetProperties().ToList(); // Get All colums of object export

                using (ExcelRange Title = workSheet.Cells[1, 1, 1, columns.Count])
                {
                    Title.Value = sheetName;
                    Title.Merge = true;
                    Title.Style.Font.Size = 15;
                    Title.Style.Font.Bold = true;
                    // Title.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // Title.Style.Fill.BackgroundColor.SetColor(BackGRColor);
                    Title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                using (ExcelRange Title = workSheet.Cells[2, 1, 2, columns.Count])
                {
                    Title.Value = DateTime.Now.ToFormalVN();
                    Title.Merge = true;
                    Title.Style.Font.Size = 15;
                    Title.Style.Font.Bold = true;
                    // Title.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // Title.Style.Fill.BackgroundColor.SetColor(BackGRColor);
                    Title.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Title.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Add title
                for (int i = 0; i < columns.Count; i++)
                {
                    // set style for title
                    workSheet.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(TitleBGRColor);
                    workSheet.Cells[3, i + 1].Style.Font.Size = 11;
                    workSheet.Cells[3, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    workSheet.Cells[3, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //set value for title
                    workSheet.Cells[3, i + 1].Value = columns[i].GetCustomAttribute<DisplayAttribute>()?.Name;
                }


                for (int row = 0; row < data.Count; row++)
                {
                    for (int column = 0; column < columns.Count; column++)
                    {
                        workSheet.Cells[row + 4, column + 1].Style.Font.Size = 10;
                        workSheet.Cells[row + 4, column + 1].Value = data[row].GetType().GetProperties()[column].GetValue(data[row]);
                    }
                }

                // Autofit column
                int minimumSize = 10;
                int maximumSize = 50;
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);

                //Add border
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[workSheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                //Remove border of title
                workSheet.Cells[2, 2, 2, columns.Count].Style.Border.Top.Style = ExcelBorderStyle.None;

                //Binding excel
                excelPackage.Save();
                return excelPackage.GetAsByteArray();
            }
        }
    }
}
