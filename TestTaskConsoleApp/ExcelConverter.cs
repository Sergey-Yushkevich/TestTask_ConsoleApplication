using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskConsoleApp
{
    public class ExcelConverter
    {
        public static byte[] Fill(IEnumerable<Item>? items)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Channel");

            sheet.Cells[1, 1].Value = "Title";
            sheet.Cells[1, 2].Value = "Link";
            sheet.Cells[1, 3].Value = "Description";
            sheet.Cells[1, 4].Value = "Category";
            sheet.Cells[1, 5].Value = "Publication date";

            if (items is not null)
            {
                var i = 1;
                foreach (var item in items)
                {
                    sheet.Cells[i + 1, 1].Value = item.Title;
                    sheet.Cells[i + 1, 2].Value = item.Link;
                    sheet.Cells[i + 1, 3].Value = item.Description;
                    sheet.Cells[i + 1, 4].Value = item.Category;
                    sheet.Cells[i + 1, 5].Value = item.PubDate.ToString();
                    i++;
                }
            }
            
            return package.GetAsByteArray();
        }
    }
}
