using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc; // Ensure this namespace is included for FileResult
using project.Manage.Dtos;
using System.IO;

namespace project.Manage.Function
{
    public static class ExcelDownloadService // Wrap the method in a class if not already part of one
    {
        public static FileContentResult DownloadWinnersExcel(List<RandomDto> winners)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Winners Report");
                // כותרות לעמודות
                worksheet.Cell(1, 1).Value = "מזהה רכישה";
                worksheet.Cell(1, 2).Value = "שם הזוכה";
                worksheet.Cell(1, 3).Value = "אימייל";
                worksheet.Cell(1, 4).Value = "טלפון";
                worksheet.Cell(1, 5).Value = "שם התרומה";

                // עיצוב הכותרות (מודגש)
                worksheet.Row(1).Style.Font.Bold = true;

                for (int i = 0; i < winners.Count; i++)
                {
                    var winner = winners[i];
                    worksheet.Cell(i + 2, 1).Value = winner.PurchaseId;
                    worksheet.Cell(i + 2, 2).Value = winner.Name;
                    worksheet.Cell(i + 2, 3).Value = winner.Email;
                    worksheet.Cell(i + 2, 4).Value = winner.Phone;
                    worksheet.Cell(i + 2, 5).Value = winner.DonationName;
                }

                // התאמת רוחב עמודות אוטומטית
                worksheet.Columns().AdjustToContents();

                // הפיכת האקסל לזרם נתונים (Stream)
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // החזרת הקובץ לדפדפן
                    return new FileContentResult(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "WinnersReport.xlsx"
                    };
                }
            }
        }
    }
}