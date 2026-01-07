using project.Manage.Dtos;

namespace project.Manage.Function
{
    public class TotalRevenue
    {
        public static byte[] CreateRevenueExcel(IEnumerable<TotalRevenueDto> reports)
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Revenue");
                worksheet.Cell(1, 1).Value = "שם תרומה";
                worksheet.Cell(1, 2).Value = "סך הכנסות";

                int row = 2;
                foreach (var item in reports)
                {
                    worksheet.Cell(row, 1).Value = item.GiftName; // שם השדה מה-DTO
                    worksheet.Cell(row, 2).Value = item.TotalRevenue; // שם השדה מה-DTO
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }

}
