using ClosedXML.Excel;
using CustomerManagement.Services.Interfaces;
using System.Data;

namespace CustomerManagement.Services.Classes
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> ExportToExcel(DataTable dataTable, string webRootPath)
        {
            string path = GetDirPath(webRootPath);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Add the column headers
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cell(1, col + 1).Value = dataTable.Columns[col].ColumnName;
                }

                // Add the data rows
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cell(row + 2, col + 1).Value = XLCellValue.FromObject(dataTable.Rows[row][col]);
                    }
                }

                // Autofit the columns
                worksheet.Columns().AdjustToContents();

                // Save the workbook to a file
                workbook.SaveAs(path);
            }

            return path;
        }

        public string GetDirPath(string webRootDir)
        {
            string path = Path.Combine(this._environment.WebRootPath, webRootDir);

            return path;
           
        }
    }
}
