using System.Data;

namespace CustomerManagement.Services.Interfaces
{
    public interface IFileService
    {
        string GetDirPath(string path);

        Task<string> ExportToExcel(DataTable dataTable, string webRootPath);
    }
}
